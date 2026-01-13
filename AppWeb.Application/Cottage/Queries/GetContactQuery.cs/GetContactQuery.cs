using MediatR;

namespace AppWeb.Application.Cottage.Queries.GetContact
{
    public class GetContactQuery : IRequest<CurrentUser>
    {
        public string EncodedName { get; set; }

        public GetContactQuery(string encodedName)
        {
            EncodedName = encodedName;
        }
    }
}