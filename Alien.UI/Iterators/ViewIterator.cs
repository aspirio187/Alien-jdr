using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alien.UI.Views;

namespace Alien.UI.Iterators
{
    public class ViewIterator : IEnumerator<ViewBase>
    {
        private ViewBase[]? _controls;

        object IEnumerator.Current => Current;

        private int _position = -1;

        public int Position
        {
            get => _position;
        }

        public ViewBase Current
        {
            get
            {
                if (_controls is null)
                {
                    throw new NullReferenceException(nameof(_controls));
                }

                if (_position < 0 || _position >= _controls.Length)
                {
                    throw new IndexOutOfRangeException(nameof(_position));
                }

                return _controls[_position];
            }
        }

        public int Length
        {
            get => _controls is not null ? _controls.Length : throw new NullReferenceException(nameof(_controls));
        }

        public ViewIterator()
        {
            _position = 0;
            _controls = new ViewBase[0];
        }

        public ViewBase? this[string name]
        {
            get
            {
                if (_controls is null)
                {
                    throw new NullReferenceException($"{nameof(_controls)} is null");
                }

                for (int i = 0; i < _controls.Length; i++)
                {
                    if (_controls[i].Name == name)
                    {
                        return _controls[i];
                    }
                }

                return null;
            }
        }

        public ViewBase this[int index]
        {
            get
            {
                if (_controls is null)
                {
                    throw new NullReferenceException($"{nameof(_controls)} is null");
                }

                if (index < 0 || index >= _controls.Length)
                {
                    throw new IndexOutOfRangeException($"{nameof(index)} range is out of controls length");
                }

                return _controls[index];
            }
        }

        public void Add(ViewBase contentControl)
        {
            if (contentControl is null)
            {
                throw new ArgumentNullException(nameof(contentControl));
            }

            if (_controls is null)
            {
                throw new NullReferenceException(nameof(_controls));
            }

            ViewBase[] controls = new ViewBase[_controls.Length + 1];

            if (_controls.Length > 0)
            {
                Array.Copy(_controls, controls, _controls.Length);
            }

            controls[^1] = contentControl;

            _controls = controls;
        }

        public bool Any(Func<ViewBase, bool> predicate)
        {
            if (_controls is null)
            {
                throw new NullReferenceException(nameof(_controls));
            }

            for (int i = 0; i < _controls.Length; i++)
            {
                if (predicate.Invoke(_controls[i]))
                {
                    return true;
                }
            }

            return false;
        }

        public void Dispose()
        {
            _controls = null;
            _position = -1;
        }

        public bool MoveNext()
        {
            if (_controls is null)
            {
                throw new NullReferenceException(nameof(_controls));
            }

            if (_position + 1 >= _controls.Length)
            {
                return false;
            }

            _position++;
            return true;
        }

        /// <summary>
        /// Move to the previous element in the iterator and remove the current element
        /// </summary>
        /// <returns>true If moving to the previous element is possible. false Otherwise</returns>
        /// <exception cref="NullReferenceException"></exception>
        public bool MovePrevious(bool removeCurrent = false)
        {
            if (_controls is null) throw new NullReferenceException(nameof(_controls));
            if (_position - 1 >= 0)
            {
                _position--;

                if (removeCurrent)
                {
                    ViewBase[] temp = new ViewBase[_controls.Length - 1];

                    Array.Copy(_controls, temp, _controls.Length - 1);

                    _controls = temp;
                }

                return true;
            }

            return false;
        }

        public void Reset()
        {
            _controls = Array.Empty<ViewBase>();
            _position = -1;
        }
    }
}