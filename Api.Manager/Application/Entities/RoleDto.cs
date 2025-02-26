using Api.Manager.Domain.Entity;

namespace Api.Manager.Application.Entities
{
    /// <summary>
    /// Data transfer object representing a role with its identifying details and status.
    /// </summary>
    public class RoleDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the role.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the role.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a description providing details about the role.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the role is active.
        /// </summary>
        public bool IsActive { get; set; }

        public static RoleDto MapFrom(RoleEntity guest)
        {
            return new RoleDto
            {
                Id = guest.Id,
                Name = guest.Name,
                Description = guest.Description,
                IsActive = guest.IsActive
            };
        }
    }
}
