namespace one_stop_shop.model;

public class BaseModel
{
    #region Properties
    public Guid Id { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }
    #endregion
}
