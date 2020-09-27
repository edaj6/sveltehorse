﻿using Domain;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoSqlDataAccess
{
    public class PersonListQueryHandler : IQueryHandler<PersonListQuery, Task<Person[]>>
    {
        private readonly Container container;

        public PersonListQueryHandler(Container container)
        {
            this.container = container ?? throw new ArgumentNullException(nameof(container));
        }

        public async Task<Person[]> Handle(PersonListQuery query)
        {
            var sqlQueryText = "SELECT TOP 20 * FROM c";

            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            FeedIterator<Person> queryResultSetIterator = this.container.GetItemQueryIterator<Person>(queryDefinition);

            List<Person> persons = new List<Person>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<Person> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                persons.AddRange(currentResultSet);
            }

            var p = persons.Where(q => q.Birthday == "260496");
            foreach (var item in p)
            {
                item.Url = "https://storagejakob.blob.core.windows.net/blob-container-test/shutterstock_228062275.png";
                await container.UpsertItemAsync(item, new PartitionKey(item.Birthday));
            }
            
            return persons.ToArray();
        }
    }
}
