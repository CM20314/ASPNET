using System;
namespace CM20314.Models
{
	/// <summary>
	/// Represents a computed route response
	/// </summary>
	public class RouteResponseData
	{
		public List<NodeArcDirection> NodeArcDirections { get; set; }
        public bool Success { get; set; }
		public string ErrorMessage { get; set; }
        public string Destination { get; set; }

        public RouteResponseData(List<NodeArcDirection> nodeArcDirections, bool success, string errorMessage, string destination)
		{
			NodeArcDirections = nodeArcDirections;
			Success = success;
			ErrorMessage = errorMessage;
			Destination = destination;
		}
	}
}

