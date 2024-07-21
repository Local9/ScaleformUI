using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using ScaleformUI.Elements;
using ScaleformUI.Scaleforms.ScaleformUI.Interfaces;
using System.Drawing;

namespace ScaleformUI.Scaleforms
{
    public class ScaleformWideScreen : INativeValue, IDisposable
    {
        private readonly IRageNatives _scaleform;

        public ScaleformWideScreen(string scaleformID)
        {
            _scaleform = Main.GetNativesHandler();
            _handle = _scaleform.RequestScaleformMovieInstance(scaleformID);
        }

        // no references to this method, any need?
        ~ScaleformWideScreen()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (IsLoaded)
            {
                _scaleform.SetScaleformMovieAsNoLongerNeeded(ref _handle);
            }

            GC.SuppressFinalize(this);
        }


        public int Handle
        {
            get { return _handle; }
        }

        private int _handle;

        public override ulong NativeValue
        {
            get
            {
                return (ulong)Handle;
            }
            set
            {
                _handle = unchecked((int)value);
            }
        }

        public bool IsValid
        {
            get
            {
                return Handle != 0;
            }
        }
        public bool IsLoaded
        {
            get
            {
                return _scaleform.HasScaleformMovieLoaded(Handle);
            }
        }

        public void CallFunction(string function, params object[] arguments)
        {
            _scaleform.BeginScaleformMovieMethod(Handle, function);
            foreach (object argument in arguments)
            {
                switch (argument)
                {
                    case int argInt:
                        _scaleform.PushScaleformMovieMethodParameterInt(argInt);
                        break;
                    case string:
                    case char:
                        if (argument.ToString().StartsWith("b_") || argument.ToString().StartsWith("t_"))
                            _scaleform.ScaleformMovieMethodAddParamPlayerNameString(argument.ToString());
                        else
                            _scaleform.PushScaleformMovieMethodParameterString(argument.ToString());
                        break;
                    case double:
                    case float:
                        _scaleform.PushScaleformMovieMethodParameterFloat((float)argument);
                        break;
                    case bool argBool:
                        _scaleform.PushScaleformMovieMethodParameterBool(argBool);
                        break;
                    case ScaleformLabel argLabel:
                        _scaleform.BeginTextCommandScaleformString(argLabel.Label);
                        _scaleform.EndTextCommandScaleformString();
                        break;
                    case ScaleformLiteralString argLiteral:
                        _scaleform.ScaleformMovieMethodAddParamTextureNameString_2(argLiteral.LiteralString);
                        break;
                    case SColor color:
                        if (color == default)
                            _scaleform.PushScaleformMovieMethodParameterInt(SColor.HUD_None.ArgbValue);
                        else
                            _scaleform.PushScaleformMovieMethodParameterInt(color.ArgbValue);
                        break;
                    default:
                        throw new ArgumentException(string.Format("Unknown argument type '{0}' passed to scaleform with handle {1}...", argument.GetType().Name, Handle), "arguments");
                }
            }
            _scaleform.EndScaleformMovieMethod();
        }

        private int CallFunctionReturnInternal(string function, params object[] arguments)
        {
            _scaleform.BeginScaleformMovieMethod(Handle, function);
            foreach (object argument in arguments)
            {
                switch (argument)
                {
                    case int argInt:
                        _scaleform.PushScaleformMovieMethodParameterInt(argInt);
                        break;
                    case string:
                    case char:
                        _scaleform.PushScaleformMovieMethodParameterString(argument.ToString());
                        break;
                    case double:
                    case float:
                        _scaleform.PushScaleformMovieMethodParameterFloat((float)argument);
                        break;
                    case bool argBool:
                        _scaleform.PushScaleformMovieMethodParameterBool(argBool);
                        break;
                    case ScaleformLabel argLabel:
                        _scaleform.BeginTextCommandScaleformString(argLabel.Label);
                        _scaleform.EndTextCommandScaleformString();
                        break;
                    case ScaleformLiteralString argLiteral:
                        _scaleform.ScaleformMovieMethodAddParamTextureNameString_2(argLiteral.LiteralString);
                        break;
                    default:
                        throw new ArgumentException(string.Format("Unknown argument type '{0}' passed to scaleform with handle {1}...", argument.GetType().Name, Handle), "arguments");
                }
            }
            return _scaleform.EndScaleformMovieMethodReturnValue();
        }
        public async Task<int> CallFunctionReturnValueInt(string function, params object[] arguments)
        {
            int ret = CallFunctionReturnInternal(function, arguments);
            while (!_scaleform.IsScaleformMovieMethodReturnValueReady(ret)) await BaseScript.Delay(0);
            return _scaleform.GetScaleformMovieFunctionReturnInt(ret);
        }
        public async Task<bool> CallFunctionReturnValueBool(string function, params object[] arguments)
        {
            int ret = CallFunctionReturnInternal(function, arguments);
            while (!_scaleform.IsScaleformMovieMethodReturnValueReady(ret)) await BaseScript.Delay(0);
            return _scaleform.GetScaleformMovieMethodReturnValueBool(ret);
        }
        public async Task<string> CallFunctionReturnValueString(string function, params object[] arguments)
        {
            int ret = CallFunctionReturnInternal(function, arguments);
            while (!_scaleform.IsScaleformMovieMethodReturnValueReady(ret)) await BaseScript.Delay(0);
            return _scaleform.GetScaleformMovieFunctionReturnString(ret);
        }

        public void Render2D()
        {
            _scaleform.DrawScaleformMovieFullscreen(Handle, 255, 255, 255, 255, 0);
        }
        public void Render2DScreenSpace(PointF location, PointF size)
        {
            float x = location.X / Screen.Width;
            float y = location.Y / Screen.Height;
            float width = size.X / Screen.Width;
            float height = size.Y / Screen.Height;

            _scaleform.DrawScaleformMovie(Handle, x + (width / 2.0f), y + (height / 2.0f), width, height, 255, 255, 255, 255, 0);
        }
        public void Render3D(Vector3 position, Vector3 rotation, Vector3 scale)
        {
            _scaleform.DrawScaleformMovie_3dNonAdditive(Handle, position.X, position.Y, position.Z, rotation.X, rotation.Y, rotation.Z, 2.0f, 2.0f, 1.0f, scale.X, scale.Y, scale.Z, 2);
        }
        public void Render3DAdditive(Vector3 position, Vector3 rotation, Vector3 scale)
        {
            _scaleform.DrawScaleformMovie_3d(Handle, position.X, position.Y, position.Z, rotation.X, rotation.Y, rotation.Z, 2.0f, 2.0f, 1.0f, scale.X, scale.Y, scale.Z, 2);
        }
    }

    public static class TypeCache<T>
    {
        static TypeCache()
        {
            Type = typeof(T);
            IsSimpleType = true;
            switch (Type.GetTypeCode(Type))
            {
                case TypeCode.Object:
                case TypeCode.DBNull:
                case TypeCode.Empty:
                case TypeCode.DateTime:
                    IsSimpleType = false;
                    break;
            }
        }

        public static bool IsSimpleType { get; }
        public static Type Type { get; }
    }
}
