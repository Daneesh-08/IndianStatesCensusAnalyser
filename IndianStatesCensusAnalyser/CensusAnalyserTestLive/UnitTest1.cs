using NUnit.Framework;
using CensusAnalyserLive.POCO;
using System.Collections.Generic;
using System.Text;
using CensusAnalyserLive;
using static CensusAnalyserLive.CensusAnalyser;

namespace CensusAnalyserTest
{
    public class Tests
    {
        static string indianStateCensusHeaders = "State,Population,AreaInSqKm,DensityPerSqKm";
        static string indianStateCodeHeaders = "SrNo,State Name,TIN,StateCode";
        static string indianStateCensusFilePath = @"C:\Users\Daneesh\source\repos\CensusAnalyserLive\CensusAnalyserTestLive\Utility\IndiaStateCensusData.csv";
        static string indianStateCodeFilePath = @"C:\Users\Daneesh\source\repos\CensusAnalyserLive\CensusAnalyserTestLive\Utility\IndiaStateCode.csv";
        static string wrongHeaderIndianCensusFilePath = @"C:\Users\Daneesh\Desktop\git\repos\IndianStatesCensusAnalyser\IndianStatesCensusAnalyser\CensusAnalyserTestLive\Utility\WrongIndiaStateCensusData.csv";
        static string delimiterIndianCensusFilePath = @"C:\Users\Daneesh\Desktop\git\repos\IndianStatesCensusAnalyser\IndianStatesCensusAnalyser\CensusAnalyserTestLive\Utility\DelimiterIndiaStateCensusData.csv";
        static string wrongIndianStateCensusFileType = @"C:\Users\Daneesh\Desktop\git\repos\IndianStatesCensusAnalyser\IndianStatesCensusAnalyser\CensusAnalyserTestLive\Utility\IndiaStateCensusData.txt";
        CensusAnalyserLive.CensusAnalyser censusAnalyser;
        Dictionary<string, CensusDTO> totalRecord;
        Dictionary<string, CensusDTO> stateRecord;
        
        [SetUp]
        public void Setup()
        {
            censusAnalyser = new CensusAnalyserLive.CensusAnalyser();
            totalRecord = new Dictionary<string, CensusDTO>();
            stateRecord = new Dictionary<string, CensusDTO>();        
        }

        [Test]
        public void GivenIndianCensusDataFile_WhenReaded_ShouldReturnCensusDataCount()
        {
            totalRecord = censusAnalyser.LoadCensusData(Country.INDIA, indianStateCensusFilePath, indianStateCensusHeaders);
            stateRecord = censusAnalyser.LoadCensusData(Country.INDIA, indianStateCodeFilePath, indianStateCodeHeaders);
            Assert.AreEqual(29, totalRecord.Count);
            Assert.AreEqual(37, stateRecord.Count);
        }
        [Test]
        public void GivenWrongFilePathThrowException()
        {
            try
            {
                totalRecord = censusAnalyser.LoadCensusData(Country.INDIA, indianStateCensusFilePath, indianStateCensusHeaders);
            }
            catch (CensusAnalyserException e)
            {
                Assert.AreEqual("File not Found", e.Message);
            }
        }
        [Test]
        public void GivenWrongFileTypeThrowException()
        {
            try
            {
                totalRecord = censusAnalyser.LoadCensusData(Country.INDIA, wrongIndianStateCensusFileType, indianStateCensusHeaders);
            }
            catch (CensusAnalyserException e)
            {
                Assert.AreEqual("Invalid File Type", e.Message);
            }
        }
        [Test]
        public void GivenWrongFileDelimiterThrowException()
        {
            try
            {
                totalRecord = censusAnalyser.LoadCensusData(Country.INDIA, delimiterIndianCensusFilePath, indianStateCensusHeaders);
            }
            catch (CensusAnalyserException e)
            {
                Assert.AreEqual("File Contains Wrong Delimiter", e.Message);
            }
        }
        [Test]
        public void GivenWrongFileHeaderThrowException()
        {
            try
            {
                totalRecord = censusAnalyser.LoadCensusData(Country.INDIA, wrongHeaderIndianCensusFilePath, indianStateCensusHeaders);
            }
            catch (CensusAnalyserException e)
            {
                Assert.AreEqual("Incorrect header in Data", e.Message);
            }
        }
    }
}