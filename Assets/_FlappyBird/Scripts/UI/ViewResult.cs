using TMPro;

namespace FlappyBird
{
    public class ViewResult
    {
        private readonly TextMeshProUGUI _current;
        private readonly TextMeshProUGUI _best;

        public ViewResult(TextMeshProUGUI current,
                          TextMeshProUGUI best)
        {
            _current = current;
            _best = best;
        }

        public TextMeshProUGUI Current => _current;
        public TextMeshProUGUI Best => _best;
    }
}
