using DevCode.Extensions.Reflection;
using Shouldly;
using System;
using Xunit;

namespace DevCode.Extensions.Tests.Reflection
{
    public class MemberInfoExtensions_Tests
    {
        [Theory]
        [InlineData(typeof(MyClass))]
        [InlineData(typeof(MyBaseClass))]
        public void GetSingleAttributeOfTypeOrBaseTypesOrNull_Test(Type type)
        {
            var attr = type.GetSingleAttributeOfTypeOrBaseTypesOrNull<SideAttribute>();
            attr.ShouldNotBeNull();
            attr.Side.ShouldBe(Sides.Host);
        }

        private class MyClass : MyBaseClass
        {

        }

        [SideAttribute(Sides.Host)]
        private abstract class MyBaseClass
        {

        }


        [Flags]
        public enum Sides
        {
            Host
        }

        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Interface)]
        public class SideAttribute : Attribute
        {
            public Sides Side { get; set; }

            public SideAttribute(Sides side)
            {
                Side = side;
            }
        }
    }
}
