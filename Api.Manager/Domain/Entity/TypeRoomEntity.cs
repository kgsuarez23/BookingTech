using Api.Manager.Base.Entity;
using Api.Manager.Domain.Records;

namespace Api.Manager.Domain.Entity
{
    public class TypeRoomEntity : Entidad<TypeRoomEntity>
    {
        public TypeRoomEntity(int id) : base(id)
        {
            Id = id;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfGuests { get; set; }

        public static TypeRoomEntity CreateFromRecord(TypeRoomRecord record)
        {
            if (record == null) throw new ArgumentNullException(nameof(record));

            return new TypeRoomEntity(record.TypeID)
            {
                Id = record.TypeID,
                Name = record.NameType,
                NumberOfGuests = record.NumberOfGuests,
                ExistsInDB = true
            };
        }

        public static TypeRoomEntity NoExistsRecord(int id)
        {
            return new TypeRoomEntity(id) { ExistsInDB = false };
        }
    }
}