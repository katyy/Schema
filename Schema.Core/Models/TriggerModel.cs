﻿namespace Schema.Core.Models
{
  public class TriggerModel
    {
      public string TableName { get; set; }
      public string TrigerName { get; set; }
      public string Event { get; set; }
      public string Type { get; set; }
      public string TypeDescription { get; set; }
    }
}