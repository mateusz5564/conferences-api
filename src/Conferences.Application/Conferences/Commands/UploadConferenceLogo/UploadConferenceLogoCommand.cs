using MediatR;

namespace Conferences.Application.Conferences.Commands.UploadConferenceLogo
{
    public class UploadConferenceLogoCommand : IRequest
    {
        public int ConferenceId { get; set; }
        public string Filename { get; set; } = default!;
        public Stream File { get; set; } = default!;
    }
}
