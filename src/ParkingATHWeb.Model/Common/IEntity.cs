namespace ParkingATHWeb.Model.Common
{
    interface IEntity<T>
    {
        T Id { get; set; }
    }
}
