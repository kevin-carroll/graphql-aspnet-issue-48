namespace Firebase.AuthTest.Controllers

{
    using GraphQL.AspNet.Attributes;
    using GraphQL.AspNet.Controllers;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using System.Linq;

    public class GraphQLController : GraphController
    {
        /*
                # Sample GraphQL Query
                query {
                    retrieveUserIdNoScheme
                }
        */
        [QueryRoot]
        [Authorize]
        public string RetrieveUserNameNoSchemeRestriction()
        {
            // successfully returns the user name
            return this.User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
        }

        /*
                # Sample GraphQL Query
                query {
                    retrieveUserIdWithSchemeRestriction
                }
        */
        [QueryRoot]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public string RetrieveUserIdWithSchemeRestriction()
        {
            // fails authorization, is not called
            return this.User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
        }
    }
}