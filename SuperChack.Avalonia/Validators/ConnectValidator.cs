using FluentValidation;
using SuperChack.Avalonia.ViewModels;

namespace SuperChack.Avalonia.Validators;

public class ConnectValidator : AbstractValidator<ConnectViewModel>
{
    public ConnectValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Введи имя.");

        RuleFor(x => x.PeerIp)
            .NotEmpty().WithMessage("Введи IP.")
            .Matches(@"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$")
            .WithMessage("Неверный формат IP. Пример: 26.131.93.127");
    }
}