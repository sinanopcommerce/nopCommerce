using Nop.Web.Framework.Models;

namespace Nop.Web.Models.Media
{
    public partial record VideoModel : BaseNopModel
    {
        public string VideoUrl { get; set; }
    }
}
