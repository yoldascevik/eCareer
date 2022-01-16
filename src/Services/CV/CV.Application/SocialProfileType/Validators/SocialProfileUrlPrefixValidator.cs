using System;
using FluentValidation;

namespace CurriculumVitae.Application.SocialProfileType.Validators;

public class SocialProfileUrlPrefixValidator : AbstractValidator<string>
{
    public SocialProfileUrlPrefixValidator()
    {
        RuleFor(x => x)
            .Must(IsValidUrl)
            .WithMessage("Url is not valid. The url must start with http or https and end with \"/\" or \"=\"");
    }

    private bool IsValidUrl(string url)
    {
        if (string.IsNullOrEmpty(url) || !url.EndsWith("/") && !url.EndsWith("="))
            return false;
            
        return Uri.TryCreate(url, UriKind.Absolute, out var uri) && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
    }
}