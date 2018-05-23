using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FeedbackApp.Repositories;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FeedbackApp
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("Cat")]
    public class CatTagHelper : TagHelper
    {
        readonly ICatRepository _catRepository;

        public CatTagHelper(ICatRepository catRepository)
        {
            _catRepository = catRepository;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            byte[] photo = null;
            photo = _catRepository.GetCatPhoto();
            if (photo != null && photo.Length > 0)
            {
                output.TagName = "img";
                output.Attributes.SetAttribute("src", $"data:image/jpeg;base64,{Convert.ToBase64String(photo)}");
            }
        }
    }
}
