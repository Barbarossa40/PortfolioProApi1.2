namespace PortfolioProApi.DataObjects.ArticlesEndpointApiDtos
{
    public class ArticleDetails
    {
        public int id { get; set; }
        public string type { get; set; }
        public DetailAtt attributes { get; set; }
        public DetailRelationships relationships { get; set; }
        public DetailLinks links { get; set; }
    }
}
