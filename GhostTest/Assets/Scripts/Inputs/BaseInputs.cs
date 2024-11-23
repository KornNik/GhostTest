namespace Controllers
{
    abstract class BaseInputs<T>
    {
        protected T _controllObject;
        public BaseInputs(T controllObject)
        {
            _controllObject = controllObject;
        }

        public abstract void UpdateControll();
    }
}
