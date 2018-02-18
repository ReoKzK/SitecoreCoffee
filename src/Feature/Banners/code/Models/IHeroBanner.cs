using Glass.Mapper.Sc.Fields;

namespace SitecoreCoffee.Feature.Banners.Models
{
    public interface IHeroBanner
    {
        string Title { get; set; }

        string Description { get; set; }

        Link Link { get; set; }

        Image Image { get; set; }
    }
}
