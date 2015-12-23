namespace ParkingATHWeb.Contracts.Common
{
    public class BaseDto<T>
        where T : struct
    {
        public T Id { get; set; }
    }
}
