﻿using AzureFunctions;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NSubstitute.Extensions;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureFunctionApp1.Tests
{
    public class HttpTriggerPersonTests
    {
        [Test]
        public async Task Run_PersonsFromPersistance_DoNotDisplayCpr()
        {
            var httpReq = Substitute.For<HttpRequest>();
            var queryHandler = Substitute.For<IQueryHandler<PersonListQuery, Task<Person[]>>> ();
            var queryResult = Task.FromResult(new Person[] { new Person { FirstName = "Mogens", CprNo = "123456-1234" } });            
            queryHandler.Handle(Arg.Any<PersonListQuery>())
                .ReturnsForAnyArgs(queryResult);
            var sut = new HttpTriggerPerson(queryHandler);

            var result = await sut.Run(httpReq) as OkObjectResult;            
            var json = JsonConvert.SerializeObject(result.Value);
            var persons = JsonConvert.DeserializeObject<Person[]>(json);

            Assert.AreEqual("Mogens", persons.FirstOrDefault().FirstName);
            Assert.AreEqual(null, persons.FirstOrDefault().CprNo);
        }
    }
}
