﻿namespace CRA.ClientLibrary.AzureProvider
{
    using CRA.ClientLibrary.DataProvider;
    using Microsoft.WindowsAzure.Storage.Table;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Definition for AzureShardedShardedVertexInfoProvider
    /// </summary>
    public class AzureShardedVertexInfoProvider
        : IShardedVertexInfoProvider
    {
        private readonly CloudTable cloudTable;

        public AzureShardedVertexInfoProvider(CloudTable cloudTable)
        {
            this.cloudTable = cloudTable;
        }

        public async Task<IEnumerable<ShardedVertexInfo>> GetAll()
            => (await cloudTable.ExecuteQueryAsync(new TableQuery<ShardedVertexTable>()))
                .Select(vt => (ShardedVertexInfo)vt);

        public async Task<int> CountAll()
            => (await GetAll()).Count();

        public async Task<ShardedVertexInfo> GetEntryForVertex(string vertexName, string epochId)
            => (await GetAll()).Where(gn => vertexName == gn.VertexName && epochId == gn.EpochId).First();

        public async Task<IEnumerable<ShardedVertexInfo>> GetEntriesForVertex(string vertexName)
        {
            var query = new TableQuery<ShardedVertexTable>()
                .Where(
                    TableQuery.GenerateFilterCondition(
                        "PartitionKey",
                        QueryComparisons.Equal,
                        vertexName));

            return (await cloudTable.ExecuteQueryAsync(query))
                .Select(vt => (ShardedVertexInfo)vt);
        }

        public async Task<ShardedVertexInfo> GetLatestEntryForVertex(string vertexName)
            => (await GetAll()).Where(gn => vertexName == gn.VertexName)
                .OrderByDescending(gn => int.Parse(gn.EpochId))
                .First();

        public Task Delete()
            => cloudTable.DeleteIfExistsAsync();

        public Task Insert(ShardedVertexInfo shardedVertexInfo)
        {
            TableOperation insertOperation =
                TableOperation.InsertOrReplace((ShardedVertexTable)shardedVertexInfo);

            return cloudTable.ExecuteAsync(insertOperation);
        }

        public Task Delete(ShardedVertexInfo entry)
            => cloudTable.ExecuteAsync(TableOperation.Delete((ShardedVertexTable)entry));
    }
}
