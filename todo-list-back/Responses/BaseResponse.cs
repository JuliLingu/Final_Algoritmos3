using todo_list_back.Models;


namespace ATDapi.Responses;

public class BaseResponse
{
    public bool succes { get; set; }
    public bool error { get; set; }
    public int code { get; set; }
    public string message { get; set; } 

    public BaseResponse(bool succes, int code, string message)
    {
        this.succes = succes;
        this.error = !succes;
        this.code = code;
        this.message = message;
    }
}
public class DataResponse<T> : BaseResponse
{
    private List<T> dataList;

    public new T data { get; set; } = default;

    public DataResponse(bool succes, int code, string message, T data = default) : base(succes, code, message)
    {
        this.data = data;
    }

    public DataResponse(bool succes, int code, string message, List<T> dataList) : base(succes, code, message)
    {
        this.dataList = dataList;
    }
}
