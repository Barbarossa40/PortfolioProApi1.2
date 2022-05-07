namespace PortfolioProApi.DataObjects.ArticlesEndpointApiDtos
{
    public class DetailRelationships
    {
        public BaseDataObj author { get; set; }

        public RelationshipDataObj sentiments { get; set; }

        public RelationshipDataObj primaryTickers { get; set; }

        public RelationshipDataObj secondaryTickers { get; set; }

        public RelationshipDataObj otherTags { get; set; }
    }
}
