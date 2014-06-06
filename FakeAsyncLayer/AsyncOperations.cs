using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FakeAsyncLayer
{
    public class AsyncOperations
    {

        /// <summary>
        /// gets a randum number async
        /// </summary>
        /// <param name="value">if value != 0 the value is returned and the thread sleeps</param>
        public async Task<int> GetRandomNumberAsync(int value = 0)
        {
            if (value == 0)
            {
                var numbers = new Random(7);
                value = numbers.Next(100);
            }
            return await LongWait(value);
        }


        /// <summary>
        /// gets a randum number async
        /// </summary>
        /// <param name="value">if value != 0 the value is returned and the thread sleeps</param>
        public int GetRandomNumber(int value = 0)
        {
            if (value == 0)
            {
                var numbers = new Random(7);
                value = numbers.Next(100);
            }
            //wait some seconds
            Thread.Sleep(value * 100);
            return value;

        }

        /// <summary>
        /// This simulates a call to a webservice or an operation against a database
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private async Task<int> LongWait(int number)
        {
            //wait some seconds
             Thread.Sleep( number * 100 ); 

            //return the same number
            return await Task.FromResult<int>(number);
        }


        public async Task<int> MultipleCalls()
        {
            //call to the async many times and add then result
            //var result = await GetRandomNumberAsync(3);
            //var result2 = await GetRandomNumberAsync(2);
            //var result3 = await GetRandomNumberAsync(1);

            //return result + result2 + result3;

            var result = new Task<int>(() => { return  GetRandomNumber(3); });
            result.Start();
            var result2 = new Task<int>(() => { return GetRandomNumber(2); });
            result2.Start();
            var result3 = new Task<int>(() => { return GetRandomNumber(1); });
            result3.Start();
            
            Task.WaitAll(new Task[] { result,result2,result3 });
            return await Task.FromResult<int>( result.Result + result2.Result + result3.Result);

        }


        public async Task<int> MultipleCallsParallelAsync()
        {
            int result = 0;
            object myLock = new ExpandoObject();

            Parallel.For(1, 4,
            f =>
            {
                //lock (myLock)
                //{
                    AddRandom (f, ref result);
                //}
            });
            return await Task.FromResult<int>(result);
        }


        public int ParallelSync()
        {
            int result = 0;
            object myLock = new ExpandoObject();
            Parallel.For(1, 4,
            f =>
            {
                //lock (myLock)
                //{
                    result += GetRandomNumber(f);
                //}
            });
            return result;
        }

        private void AddRandom(int val, ref int result)
        { 
            var val2  = GetRandomNumberAsync(val);
            val2.Wait();

            result += val2.Result;
        }
     }
}
