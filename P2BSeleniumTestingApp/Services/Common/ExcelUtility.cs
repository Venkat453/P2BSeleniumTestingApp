using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2BSeleniumTestingApp
{
    public class ExcelUtility
    {
        public static List<Datacollection> dataCol = new List<Datacollection>();
        private static DataTable resultantDataTable;

        public ExcelUtility(string filePath, string sheetName)
        {
            ExcelToDataTable(filePath, sheetName);
        }

        /// <summary>
        /// Excel to Data Table convertion 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public void ExcelToDataTable(string filePath, string sheetName)
        {
            //open file and returns as Stream
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                //Createopenxmlreader via ExcelReaderFactory
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    //Return as DataSet
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        //Set the First Row as Column Name
                        ConfigureDataTable = (data) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });

                    //Get all the Tables
                    DataTableCollection table = result.Tables;

                    //Store it in DataTable
                    resultantDataTable = new DataTable();
                    resultantDataTable = table[sheetName];
                    //return
                    //return resultantDataTable;
                }
            }
        }

        /// <summary>
        /// Populate the data in collection
        /// </summary>
        public static void DataPopulateInCollection()
        {
            DataTable table = new DataTable();
            table = resultantDataTable;
            dataCol = new List<Datacollection>();
            //Iterate through the rows and columns of the Table
            for (int row = 1; row <= table.Rows.Count; row++)
            {
                for (int col = 0; col < table.Columns.Count; col++)
                {
                    Datacollection dtTable = new Datacollection()
                    {
                        rowNumber = row,
                        colName = table.Columns[col].ColumnName,
                        colValue = table.Rows[row - 1][col].ToString()
                    };
                    //Add all the details for each row
                    dataCol.Add(dtTable);
                }
            }
        }

        /// <summary>
        /// Read data from Collection
        /// </summary>
        /// <param name="rowNumber"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static string ReadData(int rowNumber, string columnName)
        {
            try
            {
                //Retriving Data using LINQ to reduce much of iterations
                string data = (from colData in dataCol
                               where colData.colName == columnName && colData.rowNumber == rowNumber
                               select colData.colValue).SingleOrDefault();

                //var datas = dataCol.Where(x => x.colName == columnName && x.rowNumber == rowNumber).SingleOrDefault().colValue;
                return data.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        /// <summary>
        /// Get data rows count from Collection
        /// </summary>
        /// <returns></returns>
        public static int DataCount()
        {
            try
            {
                DataTable table = new DataTable();
                table = resultantDataTable;
                return table.Rows.Count;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }

    }

    public class Datacollection
    {
        public int rowNumber { get; set; }
        public string colName { get; set; }
        public string colValue { get; set; }
    }
}
