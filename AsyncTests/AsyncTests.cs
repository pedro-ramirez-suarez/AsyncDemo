using FakeAsyncLayer;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncTests
{
    public class AsyncTests
    {

        [Test]
        public async Task FakeCallTestAsync()
        {
            var asyncOps = new AsyncOperations();
            var result = await asyncOps.GetRandomNumberAsync(5);
            Assert.IsTrue(result > 0);
        }




        [Test]
        public void  FakeCallTest()
        {
            var asyncOps = new AsyncOperations();
            var result =  asyncOps.GetRandomNumber(5);
            Assert.IsTrue(result > 0);
        }
        
        
        [Test]
        public async Task FakeMultipleAsync()
        {
            var asyncOps = new AsyncOperations();
            var result = await asyncOps.MultipleCalls();
            Assert.IsTrue(result > 0);
        }


        [Test]
        public async Task FakeParallelAsync()
        {
            var asyncOps = new AsyncOperations();
            var result = await asyncOps.MultipleCallsParallelAsync();
            Assert.IsTrue(result > 0);
        }

        [Test]
        public  void FakeParallelResult()
        {
            var asyncOps = new AsyncOperations();
            var result = asyncOps.ParallelSync();
            Assert.IsTrue(result > 0);
        }
    }
}
