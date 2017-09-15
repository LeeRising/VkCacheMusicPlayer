using System;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Widget;

namespace VkMusicPlayer
{
    public sealed class IconifyTextView : TextView
    {
        public IconifyTextView(Context context) : base(context)
        {
            Init(context);
        }

        public IconifyTextView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init(context, attrs);
        }

        public IconifyTextView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Init(context, attrs);
        }

        public IconifyTextView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Init(context,attrs);
        }

        private void Init(Context context, IAttributeSet attrs = null)
        {
            SetTextAppearance(Resource.Style.IconifyTextViewStyle);
            var array = context.ObtainStyledAttributes(attrs, Resource.Styleable.IconifyTextView);
            var typeFacePath = array?.GetString(Resource.Styleable.IconifyTextView_Typeface);
            if (typeFacePath == null) return;
            Typeface = Typeface.CreateFromAsset(context.Assets, typeFacePath);
        }

        protected IconifyTextView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {

        }
    }
}