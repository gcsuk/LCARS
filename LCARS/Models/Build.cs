namespace LCARS.Models
{
	public class Build
	{
	    public int Id { get; set; }

	    public int TypeId { get; set; }

	    public string Number { get; set; }

		public string Name { get; set; }

	    private string _status;

	    public string Status
	    {
	        get { return Progress == null ? _status : string.Format("{0}% - {1}", Progress.Percentage, Progress.Status); }
	        set { _status = value; }
	    }

	    public BuildProgress Progress { get; set; }
	}
}