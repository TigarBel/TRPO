using GRPO.Drawing.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPO
{
    /// <summary>
    /// Класс отвечающий за историю CanvasControl
    /// </summary>
    [Serializable]
    class HistoryManagerCanvasControl
    {
        /// <summary>
        /// Список фигур хронящихся в памяти
        /// </summary>
        private List<IDrawable> _buferDraw;
        /// <summary>
        /// Список фигур
        /// </summary>
        private List<IDrawable> _drawables;
        /// <summary>
        /// Картинка с холста
        /// </summary>
        private Image _image;
        /// <summary>
        /// Объект взаимодействия с нарисованными фигурами
        /// </summary>
        private Interaction _interaction;
        /// <summary>
        /// Размер холста по X
        /// </summary>
        private int _sizeCanvasX;
        /// <summary>
        /// Размер холста по Y
        /// </summary>
        private int _sizeCanvasY;
        /// <summary>
        /// Заполненный класс истории CanvasControl
        /// </summary>
        /// <param name="buferDraw">Список фигур хронящихся в памяти</param>
        /// <param name="drawables">Список фигур</param>
        /// <param name="image">Картинка с холста</param>
        /// <param name="interaction">Объект взаимодействия с нарисованными фигурами</param>
        /// <param name="sizeCanvasX">Размер холста по X</param>
        /// <param name="sizeCanvasY">Размер холста по Y</param>
        public HistoryManagerCanvasControl(List<IDrawable> buferDraw, List<IDrawable> drawables, Image image, Interaction interaction,
            int sizeCanvasX, int sizeCanvasY)
        {
            _buferDraw = new List<IDrawable>(buferDraw);
            _drawables = new List<IDrawable>(drawables);
            _image = image;
            _interaction = interaction;
            _sizeCanvasX = sizeCanvasX;
            _sizeCanvasY = sizeCanvasY;
        }
        /// <summary>
        /// Список фигур
        /// </summary>
        public List<IDrawable> Drawables
        {
            get
            {
                return _drawables;
            }
        }
        /// <summary>
        /// Картинка с холста
        /// </summary>
        public Image Image
        {
            get
            {
                return new Bitmap(_image);
            }
        }
        /// <summary>
        /// Объект взаимодействия с нарисованными фигурами
        /// </summary>
        public Interaction Interaction
        {
            get
            {
                return _interaction;
            }
        }
        /// <summary>
        /// Список фигур хронящихся в памяти
        /// </summary>
        public List<IDrawable> BuferDraw
        {
            get
            {
                return _buferDraw;
            }
        }
        /// <summary>
        /// Получить ширину холста
        /// </summary>
        /// <returns>Ширину холста</returns>
        public int GetWidthCanvas()
        {
            return _sizeCanvasX;
        }
        /// <summary>
        /// Получить высоту холста
        /// </summary>
        /// <returns>Высоту холста</returns>
        public int GetHeightCanvas()
        {
            return _sizeCanvasY;
        }
        /// <summary>
        /// Перерисовать фигуры из списка
        /// </summary>
    }
}
