using EncuestasAPI2.Models;

namespace EncuestasAPI2.DTO
{
    public class RegionDTO
    {
        public int Id { get; set; }

        public string NombreRegion { get; set; } = null!;

        public static RegionDTO RegionToDTO(Region region) =>
            new RegionDTO
            {
                Id = region.Id,
                NombreRegion = region.NombreRegion
            };
    }
}
