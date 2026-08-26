using Xunit;

// Testovi dele istu SQLite bazu, pa se ne izvrsavaju paralelno
[assembly: CollectionBehavior(DisableTestParallelization = true)]
