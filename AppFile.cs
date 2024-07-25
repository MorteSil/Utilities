using System.Security;

namespace Utilities
{
    /// <summary>
    /// Base Class for all Game Files.
    /// </summary>
    public abstract class AppFile : IEquatable<AppFile>
    {

        #region Properties
        /// <summary>
        /// The Name of the <see cref="AppFile"/> this object represents.
        /// </summary>
        public string? FileName { get { return _FileInfo?.Name; } }
        /// <summary>
        /// The Path of the <see cref="AppFile"/> this object represents.
        /// </summary>
        public string? FilePath { get { return _FileInfo?.DirectoryName; } }
        /// <summary>
        /// The Extension of the <see cref="AppFile"/> this object represents.
        /// </summary>
        public string? FileExtension { get { return _FileInfo?.Extension; } }
        /// <summary>
        /// The Full Path and File Name of the <see cref="AppFile"/> this object represents.
        /// </summary>
        public string? FullName { get { return _FileInfo?.FullName; } }
        /// <summary>
        /// The Length of the <see cref="AppFile"/> this object represents.
        /// </summary>
        public long Length { get { return _FileInfo is null ? 0 : _FileInfo.Length; } }
        /// <summary>
        /// The type of <see cref="AppFile"/> this object represents.
        /// </summary>
        public ApplicationFileType FileType { get { return _FileType; } }
        /// <summary>
        /// When <see langword="true"/>, indicates the data within this file is stored in a compressed state using LZSS.
        /// </summary>
        public bool IsCompressed
        { get { return _IsCompressed; } }
        /// <summary>
        /// Inidicates if the underlying File has been modified.
        /// </summary>
        public bool IsFileModified
        {
            get { return _InitialHash != GetHashCode(); }
        }
        /// <summary>
        /// <para>When <see langword="true"/>, indicates this <see cref="AppFile"/> was successfully loaded from the file.</para>
        /// <para><see langword="false"/> indicates there were no values in the initialization data used for this <see cref="AppFile"/> object and empty or default values were loaded instead.</para>
        /// </summary>       
        public abstract bool IsDefaultInitialization
        { get; }

        #endregion Properties

        #region Fields                
        protected ApplicationFileType _FileType = ApplicationFileType.NONE;
        protected FileStreamType _StreamType = FileStreamType.Text;
        protected FileInfo? _FileInfo;
        protected bool _IsCompressed = false;
        protected int _InitialHash = 0;
        #endregion Fields

        #region Helper Methods

        /// <summary>
        /// Attempts to initialze the <see cref="AppFile"/> object from the text in <paramref name="data"/>.
        /// </summary>
        /// <param name="data">A <see cref="string"/> object with initialization data formatted as text for this <see cref="AppFile"/></param>
        /// <returns><para><see langword="true"/> if the <see cref="AppFile"/> is successfully initialized, otherwise <see langword="false"/>.</para>
        /// <exception cref="NotImplementedException"></exception>
        protected virtual bool Read(string data)
        {
            throw new NotImplementedException("This File Type requires binary data..");
             
        }
        /// <summary>
        /// Attempts to initialze the <see cref="AppFile"/> object from the <see cref="byte"/>[] in <paramref name="data"/>.
        /// </summary>
        /// <param name="data">A <see cref="byte"/> array with initialization data for this <see cref="AppFile"/>.</param>
        /// <returns><para><see langword="true"/> if the <see cref="AppFile"/> is successfully initialized, otherwise <see langword="false"/>.</para>
        /// <exception cref="NotImplementedException"></exception>
        protected virtual bool Read(byte[] data)
        {
            throw new InvalidDataException("This File Type requires string data.");
        }
        /// <summary>
        /// Formats and arranges the Data within this <see cref="AppFile"/> object for writing to a file on disk.
        /// </summary>
        /// <returns><see cref="byte[]"/> that can be written to a file</returns>
        protected abstract byte[] Write();
        #endregion Helper Methods

        #region Functional Methods
        /// <summary>
        /// Attempts to read and parse the Data from the <see cref="File"/> located at <paramref name="fileName"/>.
        /// </summary>
        /// <param name="fileName">The File to read</param>
        /// <returns><para><see langword="true"/> if the caller has the required permissions and <paramref name="fileName"/> contains the name of an existing file which is successfully parsed; otherwise, <see langword="false"/>.</para>
        /// <para>This method also returns <see langword="false"/> if <paramref name="fileName"/> is null, an invalid path, or a zero-length string. If the caller does not have sufficient permissions to read the specified file, 
        /// no exception is thrown and the method returns <see langword="false"/> regardless of the existence of path.</para></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="NotImplementedException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        public bool Load(string fileName)
        {
            try
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(fileName);               

                bool result = false;
                switch (_StreamType)
                {
                    case FileStreamType.XML:
                    case FileStreamType.Text:
                        {
                            if (!File.Exists(fileName)) throw new FileNotFoundException("The file " + fileName + "could not be found.");
                            result = Read(File.ReadAllText(fileName));

                            break;
                        }
                    case FileStreamType.Binary:
                        {
                            if (!File.Exists(fileName)) throw new FileNotFoundException("The file " + fileName + "could not be found.");
                            result = Read(File.ReadAllBytes(fileName));

                            break;

                        }
                    case FileStreamType.ProtoBuff:
                        {
                            if (!File.Exists(fileName)) throw new FileNotFoundException("The file " + fileName + "could not be found.");
                            string msg = "Protobuff Read functionality has not been fully implemented yet.";
                            throw new NotImplementedException(msg);
                        }
                    case FileStreamType.FileName:
                        {
                            if (!File.Exists(fileName)) throw new FileNotFoundException("The file " + fileName + "could not be found.");
                            result = Read(fileName);
                            break;

                        }
                    case FileStreamType.DirectoryName:
                        {
                            if (!Directory.Exists(fileName)) throw new FileNotFoundException("The file " + fileName + "could not be found.");
                            result = Read(fileName);
                            break;
                        }
                }

                if (!result) throw new SecurityException("Unable to access the file at " + fileName);
                _FileInfo = new FileInfo(fileName);
                return true;

            }
            catch (Exception ex)
            {
                Logging.ErrorLog.CreateLogFile(ex, "This error occurred while trying to Load a file at " + fileName);
                if (ex is ArgumentException || ex is SecurityException)
                    return false;

                throw;
            }
        }

        /// <summary>
        /// Attempts to open or create the file at <paramref name="fileName"/> and write the contents of this file to it.
        /// </summary>
        /// <param name="fileName">File name to write the data to.</param>
        /// <returns><see langword="true"/> if the file was successfully saved, <see langword="false"/> otherwise.</returns>
        /// <exception cref="NotImplementedException">Indicates the Save funciton is not implemented for this file type yet.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="fileName"/> is empty or null.</exception>
        public virtual bool Save(string fileName)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(fileName);

            try
            {
                if (FileType == ApplicationFileType.DatabaseOCD)
                {
                    Write();
                    return true;
                }
                    
                byte[] data = Write();

                if (data is null || data.Length == 0) return false;

                File.WriteAllBytes(fileName, data);
            }
            catch (Exception ex)
            {
                Logging.ErrorLog.CreateLogFile(ex, "This error occurred while trying to write to " + fileName);
                if (ex is IOException)
                    return false;
                if (ex is SecurityException)
                    return false;
                throw;
            }


            // File successfully written   
            _FileInfo = new FileInfo(fileName);
            // Notify caller of success
            return true;
        }

        /// <summary>
        /// <para>Formats the data contained within the <see cref="AppFile"/> object into Readable Text.</para>
        /// <para>Readable Text does not always match the underlying file format and should not be used to save text based files such as .ini, .lst, or .txtpb files.</para>
        /// <para>Instead, use Write() to format all text or binary data for writing to a file.</para>
        /// </summary>
        /// <returns>A formatted <see cref="string"/> with the Data contained within the <see cref="AppFile"/> object.</returns>
        public new abstract string ToString();

        #region Equality Methods
        public virtual bool Equals(AppFile? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (other.GetType() != GetType()) return false;

            return other.ToString() == ToString() && other.GetHashCode() == GetHashCode();
        }
        public abstract override bool Equals(object? other);
        public abstract override int GetHashCode();
        public static bool operator ==(AppFile comparator1, AppFile comparator2)
        {
            if ((object)comparator1 == null || (object)comparator2 == null)
                return Equals(comparator1, comparator2);

            return comparator1.Equals(comparator2);
        }
        public static bool operator !=(AppFile comparator1, AppFile comparator2)
        {
            if ((object)comparator1 == null || (object)comparator2 == null)
                return !Equals(comparator1, comparator2);

            return !comparator1.Equals(comparator2);
        }

        #endregion Equality Methods

        #endregion Functional Methods

        #region Constructors

        #endregion Constructors

    }
}
