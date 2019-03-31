using System;
using System.Collections.Generic;

namespace yase_core.Models
{
    public struct Option<T> : IEquatable<Option<T>>
    {
        private readonly Boolean _hasValue;
        private readonly T _value;

        internal Option(T value, Boolean hasValue)
        {
            _value = value;
            _hasValue = hasValue;
        }

        public Boolean HasValue
        {
            get { return _hasValue; }
        }

        internal T Value
        {
            get { return _value; }
        }

        public static Boolean operator ==(Option<T> left, Option<T> right)
        {
            return left.Equals(right);
        }

        public static Boolean operator !=(Option<T> left, Option<T> right)
        {
            return !left.Equals(right);
        }

        public Boolean Equals(Option<T> other)
        {
            if (!_hasValue && !other._hasValue)
                return true;

            if (_hasValue && other._hasValue)
                return EqualityComparer<T>.Default.Equals(_value, other._value);

            return false;
        }

        public override Boolean Equals(Object obj)
        {
            return obj is Option<T> && Equals((Option<T>)obj);
        }

        public override Int32 GetHashCode()
        {
            if (_hasValue)
                return _value == null ? 1 : _value.GetHashCode();

            return 0;
        }

        public override String ToString()
        {
            if (_hasValue)
                return _value == null ? "Some(null)" : String.Format("Some({0})", _value);

            return "None";
        }

        public IEnumerable<T> ToEnumerable()
        {
            if (_hasValue)
                yield return _value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (_hasValue)
                yield return _value;
        }

        public Boolean Contains(T value)
        {
            if (_hasValue)
                return _value == null ? value == null : _value.Equals(value);

            return false;
        }

        public Boolean Exists(Func<T, Boolean> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("predicate");

            return _hasValue && predicate(_value);
        }

        public T ValueOr(T @default)
        {
            return _hasValue ? _value : @default;
        }

        public T ValueOr(Func<T> @default)
        {
            if (@default == null)
                throw new ArgumentNullException("default");

            return _hasValue ? _value : @default();
        }

        public Option<T> Or(T @default)
        {
            return _hasValue ? this : Option.Some(@default);
        }

        public Option<T> Or(Func<T> @default)
        {
            if (@default == null)
                throw new ArgumentNullException("default");

            return _hasValue ? this : Option.Some(@default());
        }

        public Option<T> Else(Option<T> @default)
        {
            return _hasValue ? this : @default;
        }

        public Option<T> Else(Func<Option<T>> @default)
        {
            if (@default == null)
                throw new ArgumentNullException("default");

            return _hasValue ? this : @default();
        }

        public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none)
        {
            if (some == null)
                throw new ArgumentNullException("some");

            if (none == null)
                throw new ArgumentNullException("none");

            return _hasValue ? some(_value) : none();
        }

        public void Match(Action<T> some)
        {
            if (some == null)
                throw new ArgumentNullException("some");

            if (_hasValue) some(_value);
        }

        public void Match(Action<T> some, Action none)
        {
            if (some == null)
                throw new ArgumentNullException("some");

            if (none == null)
                throw new ArgumentNullException("none");

            if (_hasValue)
            {
                some(_value);
            }
            else
            {
                none();
            }
        }

        public Option<TResult> Map<TResult>(Func<T, TResult> map)
        {
            if (map == null)
                throw new ArgumentNullException("map");

            return Match(some: _ => Option.Some(map(_)),
                         none: Option.None<TResult>);
        }

        public Option<TResult> Map<TResult>(Func<T, TResult> map, Predicate<TResult> noneWhen )
        {
            if (map == null)
                throw new ArgumentNullException("map");
           
            return Match(some: _ => {
                                    var mapped = map(_);
                                    return noneWhen(mapped) ? 
                                        Option.None<TResult>() : Option.Some(mapped);
                                    },
                         none: Option.None<TResult>);
        }
        
        public Option<TResult> FlatMap<TResult>(Func<T, Option<TResult>> map)
        {
            if (map == null)
                throw new ArgumentNullException("map");

            return Match(some: map,
                         none: Option.None<TResult>);
        }
    }

    public static class Option
    {
        public static Option<T> ToOption<T>(this T value)
        {
            return ToOption<T>(value, _ => Equals(_, default(T)));
        }

        public static Option<T> ToOption<T>(this T value, Predicate<T> noneWhen)
        {
            return noneWhen(value) ? None<T>() : Some(value);
        }

        public static Option<TResult> ToOption<TInput, TResult>(this TInput value,
                                                                Func<TInput, TResult> map)
        {
            return ToOption(value, map, _ => Equals(_, default(TInput)));
        }

        public static Option<TResult> ToOption<TInput, TResult>(this TInput value,
                                                                Func<TInput, TResult> map,
                                                                Predicate<TInput> noneWhen)
        {
            return noneWhen(value) ? None<TResult>() : Some(map(value));
        }

        public static Option<T> Some<T>(T value)
        {
            return new Option<T>(value, true);
        }

        public static Option<T> None<T>()
        {
            return new Option<T>(default(T), false);
        }
    }
}