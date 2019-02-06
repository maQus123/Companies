namespace Companies.TagHelpers {

    using Microsoft.AspNetCore.Razor.TagHelpers;

    public class TableHeadlineTagHelper : TagHelper {
        
        public string CurrentPage { get; set; }

        public string Key { get; set; }

        public string SortBy { get; set; }

        public string Value { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output) {
            output.TagName = "a";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.SetAttribute("class", "text-dark");
            var content = this.Value;
            if (this.Key == this.SortBy) {
                content += " ↓";
            } else {
                var href = "/?sortby=" + this.Key;
                if (!string.IsNullOrEmpty(this.CurrentPage)) {
                    href += "&currentpage=" + this.CurrentPage;
                }
                output.Attributes.SetAttribute("href", href);
            }            
            output.Content.SetContent(content);
            return;
        }

    }

}