using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AI_riddle
{
    public static class TableStorageService
    {
        static CloudStorageAccount cloudStorageAccount;
        static CloudTableClient tableClient;
        static CloudTable songsTable;


        private static async Task ConnectToTable()
        {
            cloudStorageAccount = CloudStorageAccount.Parse(Constants.StorageAccountConnectionString);
            tableClient = cloudStorageAccount.CreateCloudTableClient();
            songsTable = tableClient.GetTableReference(Constants.riddlesTableName);
            await songsTable.CreateIfNotExistsAsync();
        }

        public static async Task<List<QA>> GetRiddles()
        {
            await ConnectToTable();

            TableContinuationToken continuationToken = null;
            var QAs = new List<QA>();

            try
            {
                do
                {
                    var result = await songsTable.ExecuteQuerySegmentedAsync(new TableQuery<QA>(), continuationToken);
                    continuationToken = result.ContinuationToken;
                    

                    if (result.Results.Count > 0)
                        QAs.AddRange(result.Results);
                } while (continuationToken != null);
            }
            catch (Exception ex)
            {

            }

            return QAs;
        }

        public static async Task<QA> GetRiddle(string partitionKey, string rowKey)
        {
            try
            {
                await ConnectToTable();
                var operation = TableOperation.Retrieve<QA>(partitionKey, rowKey);
                var query = await songsTable.ExecuteAsync(operation);
                return query.Result as QA;
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public static async Task<bool> SaveRiddle(QA qa)
        {
            try
            {
                await ConnectToTable();
                var operation = TableOperation.Insert(qa);
                var upsert = await songsTable.ExecuteAsync(operation);
                return (upsert.HttpStatusCode == 204);
            }
            catch (Exception ex)
            {

            }

            return false;
        }

        public static async Task<bool> DeleteRiddle(QA qa)
        {
            try
            {
                await ConnectToTable();
                var operation = TableOperation.Delete(qa);
                var delete = await songsTable.ExecuteAsync(operation);
                return (delete.HttpStatusCode == 204);
            }
            catch (Exception ex)
            {

            }

            return false;
        }
    }
}
