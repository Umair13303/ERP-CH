using IMSClothHouse.Models;

namespace IMSClothHouse.Interfaces
{
    public interface IStoredProcedure
    {
        public int? upsert_Into_CBranch(SQLParameter postedData);
    }
}

