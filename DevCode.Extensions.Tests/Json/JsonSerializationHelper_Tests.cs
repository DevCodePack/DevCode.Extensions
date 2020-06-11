using DevCode.Extensions.Json;
using DevCode.Extensions.Timing;
using Shouldly;
using System;
using Xunit;

namespace DevCode.Extensions.Tests.Json
{
    public class JsonSerializationHelper_Tests
    {
        [Fact]
        public void Should_Deserialize_With_DateTime()
        {
            Clock.Provider = ClockProviders.Utc;

            var str = "DevCode.Extensions.Tests.Json.JsonSerializationHelper_Tests+MyClass2, DevCode.Extensions.Tests, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null|{\"Date\":\"2020-06-12T20:20:10.226+08:00\"}";
            var result = (MyClass2)JsonSerializationHelper.DeserializeWithType(str);
            result.ShouldNotBeNull();
            result.Date.ShouldBe(new DateTime(2020, 06, 12, 12, 20, 10, 226, Clock.Kind));
            result.Date.Kind.ShouldBe(Clock.Kind);
        }

        [Fact]
        public void Should_Deserialize_Without_DateTime_Normalization()
        {
            Clock.Provider = ClockProviders.Utc;

            var str1 = "DevCode.Extensions.Tests.Json.JsonSerializationHelper_Tests+MyClass3, DevCode.Extensions.Tests, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null|{\"Date\":\"2020-06-10T16:58:10.526+08:00\"}";
            var result1 = (MyClass3)JsonSerializationHelper.DeserializeWithType(str1);
            result1.ShouldNotBeNull();
            result1.Date.Kind.ShouldBe(DateTimeKind.Local);

            var str2 = "DevCode.Extensions.Tests.Json.JsonSerializationHelper_Tests+MyClass4, DevCode.Extensions.Tests, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null|{\"Date\":\"2020-06-10T16:58:10.526+08:00\"}";
            var result2 = (MyClass4)JsonSerializationHelper.DeserializeWithType(str2);
            result2.ShouldNotBeNull();
            result2.Date.Kind.ShouldBe(DateTimeKind.Local);
        }

        public class MyClass1
        {
            public string Name { get; set; }

            public MyClass1()
            {

            }

            public MyClass1(string name)
            {
                Name = name;
            }
        }

        public class MyClass2
        {
            public DateTime Date { get; set; }

            public MyClass2(DateTime date)
            {
                Date = date;
            }
        }

        [DisableDateTimeNormalization]
        public class MyClass3
        {
            public DateTime Date { get; set; }

            public MyClass3(DateTime date)
            {
                Date = date;
            }
        }

        public class MyClass4
        {
            [DisableDateTimeNormalization]
            public DateTime Date { get; set; }

            public MyClass4(DateTime date)
            {
                Date = date;
            }
        }
    }
}