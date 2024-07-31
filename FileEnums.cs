using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    /// <summary>
    /// Type of data conatined within a file
    /// </summary>
    public enum FileStreamType
    {
        /// <summary>
        /// File contents are stored in plain text.
        /// </summary>
        Text,
        /// <summary>
        /// File contents are encoded into standard binary formats for x86/x64 value types.
        /// </summary>
        Binary,
        /// <summary>
        /// File contents are encoded in Binary ProtoBuff format.
        /// </summary>
        ProtoBuff,
        /// <summary>
        /// File contents are encoded in Plain Text Proto Format.
        /// </summary>
        ProtoText,
        /// <summary>
        /// File Contents are stored as plain text that adheres to the XML/XSD Specifications.
        /// </summary>
        XML,
        /// <summary>
        /// Read Function requires a File Name instead of raw data.
        /// </summary>
        FileName,
        /// <summary>
        /// Read Function requuires a Directory Name instead of raw data.
        /// </summary>
        DirectoryName
    }

    /// <summary>
    /// Available File Types
    /// </summary>
    public enum ApplicationFileType
    {
        NONE = -1,

        AcDAT,
        AcAFM,
        ACPROTO, 
        ACTypesLST,        
        ATCAirbaseLST,
        ATCDAT,
        BMSCFG,
        BMSUserCFG,
        BombDAT,
        BombTypesLST,
        CampaignAII,        
        CampaignCAM,  // Campaign Wrapper File .cam
        CampaignCMP, // Embedded Campaign Data within CAM .cmp
        CampaignEVT, // Embedded Event list within CAM .evt
        CampaignFRC,  // Force Ration File .frc
        CampaignHIS,  // History File .his
        CampaignIFF,  // IFF Plan File .iff
        CampaignINI,  // Same as TEMissionINI .ini
        TEMissionINI = CampaignINI,  // Mission or TE DTC File .ini
        CampaignInvalidAC,
        CampaignL16,
        CampaignMissionData, // Sets mission Parameters for each Mission Type
        CampaignOBJ, // Embedded Campaign Objective List within CAM .obj
        CampaignOBD, // Embedded Campaign Objective Deltas data within CAM .obd
        CampaignPLT, // Embedded Pilot List within CAM .plt
        CampaignPOL, // Embedded Primary Objectives Lisr within CAM .pol
        CampaignPRI, // Priotities File .pri (Attrit, CAS, Defense Intdict, Offense)
        CampaignPROTO,
        CampaignPST, // Embedded Persistent Object List within CAM .pst
        CampaignRAD, // radiomap.dat
        CampaignRT,
        CampaignStrings,
        CampaignTAC,
        CampaignTE, // Embedded Victory Conditions within CAM .te
        CampaignTEA, // Embedded Teams list within CAM .tea
        CampaignTENavalUnits,
        CampaignTEPlanes,
        CampaignTEUnits,
        CampaignTRI,
        CampaignTT,
        CampaignTWX,
        CampaignUNI, // Embedded Unit List withing CAM .uni
        CampaignVER, // Embedded Verison Info within CAM .ver
        CampaignWeatherCells,
        CampaignWTH, // Embedded Weather within CAM .wth
        DatabaseCT,
        DatabaseACD,
        DatabaseDDP,
        DatabaseFCD,
        DatabaseFED,
        DatabaseICD,
        DatabaseOCD,
        DatabasePDX,
        DatabasePHD,
        DatabaseRCD,
        DatabaseRKT,
        DatabaseRWD,
        DatabaseSSD,
        DatabaseSWD,
        DatabaseUCD,
        DatabaseVCD,
        DatabaseVSD,
        DatabaseWCD,
        DatabaseWLD,
        DTCINI, // DTC File .ini
        FormDatFIL,
        IRSTLST,
        Link16PROTO, // Link 16 Plan .txtpb
        ListLST,
        MissileDAT,
        MisTypesLST,
        RadarDAT,
        RadTypesLST,
        RWRLST,
        SensorIRS,
        SensorRWR,
        SensorVSS,
        SIGDATALAST,
        SignatureFile,
        StationsDAT,
        TacticalTAC, // Tactical Engagement File (Same structure as CAM
        TerrainBUL,
        TerrainHDR,
        TerrainNRM,
        TerrainSBI,
        TerrainTID,
        TextureBIN,
        TheaterMAP,
        TheaterNAM,
        TheaterPAK,
        TheaterTHR,
        TheaterTM,
        TheaterIRC, // Necessary?
        TheaterMEA,
        TheaterL2,
        TheaterO2,
        TheaterTDF,
        TrainingTRN, // Training Mission File (Same structure as CAM
        VehicleLST,
        VehicleVEH,
        VISUALLST,
        WeatherINI,        
        WeatherFMAP,

    }
}
