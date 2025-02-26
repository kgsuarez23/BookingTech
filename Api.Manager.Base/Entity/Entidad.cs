namespace Api.Manager.Base.Entity
{
    public class Entidad<T>
    {
        public Entidad(int id)
        {
            IdEntidad = id;
        }

        public bool ExistsInDB { get; protected set; } = true;
        public int IdEntidad { get; protected set; }
    }
}
