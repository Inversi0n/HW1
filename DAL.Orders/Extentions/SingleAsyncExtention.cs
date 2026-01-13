namespace DAL.Orders.Extentions
{
    public static class SingleAsyncExtention
    {

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="executionTask"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">If result is null, empty, it's count more than 1, or the single value is null</exception>
        public static async Task<T> GetSingleAsync<T>(this Task<T[]> executionTask)
        {
            var valuesA = await executionTask;

            if (valuesA == null || valuesA.Length != 1)
                throw new ArgumentException($"Not found correct results for {typeof(T).Name}. Count {valuesA?.Length}");

            var res = valuesA.Single();
            if (res == null)
                throw new ArgumentException($"Found null instead of correct result for type {typeof(T).Name}");

            return res;
        }

        //public static async Task<T> GetSingle<T>(this Task<IEnumerable<T>> executionTask)
        //{
        //    var valuesE = await executionTask;
        //    var valuesA = valuesE?.ToArray();

        //    if (valuesA == null || valuesA.Length != 1)
        //        throw new ArgumentException($"Not found correct results for {typeof(T).Name}. Count {valuesA?.Length}");

        //    return valuesA.Single();
        //}
    }
}
