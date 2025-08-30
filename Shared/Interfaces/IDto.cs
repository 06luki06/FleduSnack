namespace At.luki0606.FleduSnack.Shared.Interfaces
{
    public interface IDto<out TResponse>
    {
        TResponse ToResponseDto();
    }
}
