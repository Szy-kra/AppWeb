using MediatR;

namespace AppWeb.Application.Cottage.Commands.DeleteCottage
{
    // DODAJ <Unit> TUTAJ
    public class DeleteCottageCommand : IRequest<Unit>
    {
        public string EncodedName { get; }

        public DeleteCottageCommand(string encodedName)
        {
            EncodedName = encodedName;
        }
    }
}