namespace back.Dtos
{
    public class RecommendedParams
    {
        private readonly bool _isDoctorPriority;
        private readonly long _doctorId;
        private readonly string _start;
        private readonly string _end;


        public RecommendedParams(bool isDoctorPriority, long doctorId, string start, string end)
        {
            _isDoctorPriority = isDoctorPriority;
            _doctorId = doctorId;   
            _start = start; 
            _end = end;
        }

        public bool IsDoctorPriority { get { return _isDoctorPriority; } }
        public long DoctorId{ get { return _doctorId; } }
        public string Start { get { return _start; } }
        public string End { get { return _end; } }

    }
}
