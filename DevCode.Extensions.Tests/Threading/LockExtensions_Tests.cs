﻿using DevCode.Extensions.Threading;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace DevCode.Extensions.Tests.Threading
{
    public class LockExtensions_Tests
    {
        private readonly List<int> _list;

        public LockExtensions_Tests()
        {
            _list = new List<int> { 1 };
        }

        [Fact]
        public void Test_Locking()
        {
            //Just sample usages:
            _list.Locking(() => { });
            _list.Locking(list => { });
            _list.Locking(() => 42).ShouldBe(42);
            _list.Locking(list => 42).ShouldBe(42);
        }
    }
}
