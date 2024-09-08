namespace Common
{
    public class Timer
    {
        private float m_CurrentTime;

        public Timer(float startTime)
        {
            Start(startTime);
        }

        public bool IsFinished => m_CurrentTime <= 0;

        public void Start(float startTime)
        {
            m_CurrentTime = startTime;
        }

        public void RemoveTime(float deltaTime)
        {
            if (m_CurrentTime <= 0) return;

            m_CurrentTime -= deltaTime;
        }
    }
}
