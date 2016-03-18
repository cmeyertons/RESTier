﻿// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License.  See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.OData.Client;
using Xunit;

namespace Microsoft.Restier.WebApi.Test.Scenario
{
    public class PropertyAccessTests : TrippinE2ETestBase
    {
        [Fact]
        public void QueryIntPropertyOfSingleEntity()
        {
            TestPayloadString("People(1)/PersonId", payloadStr =>
            {
                Assert.Contains(
                    "\"@odata.context\":\"http://localhost:18384/api/Trippin/$metadata#People(1)/PersonId\"," +
                    "\"value\":1", payloadStr, StringComparison.Ordinal);
            });
        }

        [Fact]
        public void QueryRawIntPropertyOfSingleEntity()
        {
            TestPayloadString("People(1)/PersonId/$value", payloadStr =>
            {
                Assert.Equal("1", payloadStr, StringComparer.Ordinal);
            });
        }

        [Fact]
        public void QueryStringPropertyOfSingleEntity()
        {
            TestPayloadString("People(1)/UserName", payloadStr =>
            {
                Assert.Contains(
                    "\"@odata.context\":\"http://localhost:18384/api/Trippin/$metadata#People(1)/UserName\"," +
                    "\"value\":\"russellwhyte\"", payloadStr, StringComparison.Ordinal);
            });
        }

        [Fact]
        public void QueryRawStringPropertyOfSingleEntity()
        {
            TestPayloadString("People(1)/UserName/$value", payloadStr =>
            {
                Assert.Equal("russellwhyte", payloadStr, StringComparer.Ordinal);
            });
        }

        [Fact]
        public void QueryDatePropertyOfSingleEntity()
        {
            TestPayloadString("People(1)/BirthDate", payloadStr =>
            {
                Assert.Contains(
                    "\"@odata.context\":\"http://localhost:18384/api/Trippin/$metadata#People(1)/BirthDate\"," +
                    "\"value\":\"1980-10-15\"", payloadStr, StringComparison.Ordinal);
            });
        }

        [Fact]
        public void QueryRawDatePropertyOfSingleEntity()
        {
            TestPayloadString("People(1)/BirthDate/$value", payloadStr =>
            {
                Assert.Equal("1980-10-15", payloadStr, StringComparer.Ordinal);
            });
        }

        [Fact]
        public void QueryTimeOfDayPropertyOfSingleEntity()
        {
            TestPayloadString("People(1)/BirthTime", payloadStr =>
            {
                Assert.Contains(
                    "\"@odata.context\":\"http://localhost:18384/api/Trippin/$metadata#People(1)/BirthTime\"," +
                    "\"value\":\"02:03:04.0000000\"", payloadStr, StringComparison.Ordinal);
            });
        }

        [Fact]
        public void QueryRawTimeOfDayPropertyOfSingleEntity()
        {
            TestPayloadString("People(1)/BirthTime/$value", payloadStr =>
            {
                Assert.Equal("02:03:04.0000000", payloadStr, StringComparer.Ordinal);
            });
        }

        [Fact]
        public void QueryNullableTimeOfDayPropertyOfSingleEntity()
        {
            TestPayloadString("People(4)/BirthTime2", payloadStr =>
            {
                Assert.Contains(
                    "\"@odata.context\":\"http://localhost:18384/api/Trippin/$metadata#People(4)/BirthTime2\"," +
                    "\"value\":\"23:59:01.0000000\"", payloadStr, StringComparison.Ordinal);
            });
        }

        [Fact]
        public void QueryNullRawTimeOfDayPropertyOfSingleEntity()
        {
            TestPayloadString("People(1)/BirthTime2/$value", payloadStr =>
            {
                Assert.Equal(string.Empty, payloadStr, StringComparer.Ordinal);
            });
        }

        [Fact]
        public void QueryDateTimeOffsetPropertyOfSingleEntity()
        {
            TestPayloadString("People(1)/BirthDateTime", payloadStr =>
            {
                Assert.Contains(
                    "\"@odata.context\":\"http://localhost:18384/api/Trippin/$metadata#People(1)/BirthDateTime\"," +
                    "\"value\":\"1980-10-15T02:03:04Z\"", payloadStr, StringComparison.Ordinal);
            });
        }

        [Fact]
        public void QueryRawDateTimeOffsetPropertyOfSingleEntity()
        {
            TestPayloadString("People(1)/BirthDateTime/$value", payloadStr =>
            {
                Assert.Equal("1980-10-15T02:03:04Z", payloadStr, StringComparer.Ordinal);
            });
        }

        [Fact]
        public void QueryNullableDateTimeOffsetPropertyOfSingleEntity()
        {
            TestPayloadString("People(4)/BirthDateTime2", payloadStr =>
            {
                Assert.Contains(
                    "\"@odata.context\":\"http://localhost:18384/api/Trippin/$metadata#People(4)/BirthDateTime2\"," +
                    "\"value\":\"1985-01-10T23:59:01Z\"", payloadStr, StringComparison.Ordinal);
            });
        }

        [Fact]
        public void QueryNullRawDateTimeOffsetPropertyOfSingleEntity()
        {
            TestPayloadString("People(1)/BirthDateTime2/$value", payloadStr =>
            {
                Assert.Equal(string.Empty, payloadStr, StringComparer.Ordinal);
            });
        }

        [Fact]
        public void QueryComplexPropertyOfSingleEntity()
        {
            var firstEvent = this.TestClientContext.Events.First();
            TestPayloadString("Events(" + firstEvent.Id + ")/OccursAt", payloadStr =>
            {
                Assert.Contains(
                    "\"@odata.context\":\"http://localhost:18384/api/Trippin/$metadata#Events(" + firstEvent.Id + ")/OccursAt\"," +
                    "\"Address\":\"Address1\"", payloadStr, StringComparison.Ordinal);
            });
        }

        [Fact]
        public void QueryPrimitivePropertyUnderComplexPropertyOfSingleEntity()
        {
            var firstEvent = this.TestClientContext.Events.First();
            TestPayloadString("Events(" + firstEvent.Id + ")/OccursAt/Address", payloadStr =>
            {
                Assert.Contains(
                    "\"@odata.context\":\"http://localhost:18384/api/Trippin/$metadata#Events(" + firstEvent.Id + ")/OccursAt/Address\"," +
                    "\"value\":\"Address1\"", payloadStr, StringComparison.Ordinal);
            });
        }

        [Fact]
        public void QueryRawPrimitivePropertyUnderComplexPropertyOfSingleEntity()
        {
            var firstEvent = this.TestClientContext.Events.First();
            TestPayloadString("Events(" + firstEvent.Id + ")/OccursAt/Address/$value", payloadStr =>
            {
                Assert.Equal("Address1", payloadStr, StringComparer.Ordinal);
            });
        }

        [Fact]
        public void QueryEnumPropertyOfSingleEntity()
        {
            TestPayloadString("People(1)/FavoriteFeature", payloadStr =>
            {
                Assert.Contains(
                    "\"@odata.context\":\"http://localhost:18384/api/Trippin/$metadata#People(1)/FavoriteFeature\"," +
                    "\"value\":\"Feature1\"", payloadStr, StringComparison.Ordinal);
            });
        }

        [Fact]
        public void QueryNullableEnumPropertyOfSingleEntity()
        {
            TestPayloadString("People(4)/FavoriteFeature2", payloadStr =>
            {
                Assert.Contains(
                    "\"@odata.context\":\"http://localhost:18384/api/Trippin/$metadata#People(4)/FavoriteFeature2\"," +
                    "\"value\":\"Feature4\"", payloadStr, StringComparison.Ordinal);
            });
        }

        [Fact]
        public void QueryNullEnumPropertyOfSingleEntity()
        {
            TestGetStatusCodeIs("People(1)/FavoriteFeature2", 204);
        }

        [Fact]
        public void QueryRawEnumPropertyOfSingleEntity()
        {
            TestPayloadString("People(1)/FavoriteFeature/$value", payloadStr =>
            {
                Assert.Equal("Feature1", payloadStr, StringComparer.Ordinal);
            });
        }

        [Fact]
        public void QueryNullableRawEnumPropertyOfSingleEntity()
        {
            TestPayloadString("People(4)/FavoriteFeature2/$value", payloadStr =>
            {
                Assert.Equal("Feature4", payloadStr, StringComparer.Ordinal);
            });
        }

        [Fact]
        public void QueryNullRawEnumPropertyOfSingleEntity()
        {
            TestGetStatusCodeIs("People(1)/FavoriteFeature2/$value", 204);
        }

        /// Note: 1.null collection of any type (primitive/enum/Complex/navCollection) is not tested yet.
        /// 2. No test case of collection with primitive/enum/complex as EF does not support
        /// 3. Complex can not be null in EF
        [Theory]
        // Single primitive property with null value 
        [InlineData("/People(4)/LastName", 204)]
        // Single primitive property $value with null value 
        [InlineData("/People(4)/LastName/$value", 204)]
        // single navigation property with null value
        // TODO Should be 204, cannot differentiate ~/People(nonexistkey) vs /People(5)/NullSingNav now
        [InlineData("/People(4)/BestFriend", 404)]
        // single navigation property's propery and navigation property has null value
        // TODO should be 404
        [InlineData("/People(4)/BestFriend/LastName", 204)]
        // single navigation property's property with null value
        [InlineData("/People(5)/BestFriend/LastName", 204)]
        // collection of navigation property with empty collection value
        [InlineData("/People(5)/Friends", 200)]
        public void QueryPropertyWithNullValueStatusCode(string url, int expectedCode)
        {
            TestGetStatusCodeIs(url, expectedCode);
        }

        private void TestPayloadString(string uriAfterServiceRoot, Action<string> testMethod)
        {
            var requestMessage = new HttpWebRequestMessage(
                new DataServiceClientRequestMessageArgs(
                    "GET",
                    new Uri(this.ServiceBaseUri, uriAfterServiceRoot),
                    true,
                    false,
                    new Dictionary<string, string>()));
            using (var r = new StreamReader(requestMessage.GetResponse().GetStream()))
            {
                var payloadStr = r.ReadToEnd();
                testMethod(payloadStr);
            }
        }
    }
}
