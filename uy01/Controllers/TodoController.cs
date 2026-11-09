using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using uy01.Models;
using uy01.Repositories;
using uy01.ViewModels;

namespace uy01.Controllers
{
    public class TodoController : Controller
    {
        private readonly TodoRepository _todoRepository;
        private readonly IMapper _mapper;
        ResultModel _result=new ResultModel();
        public TodoController(TodoRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
          var todos = await _todoRepository.GetAllAsync();
            var todoModels = _mapper.Map<List<TodoModel>>(todos);    

            return Json(todoModels);
        }

        public async Task<IActionResult> GetById(int id)
        {
            var todo = await _todoRepository.GetByIdAsync(id);
            var todoModel = _mapper.Map<TodoModel>(todo);
            return Json(todoModel);
        }

        public async Task<IActionResult> AddUpdate(TodoModel model)
        {
            if (model.Id == 0)
            {
                var todo = _mapper.Map<Todo>(model);
                await _todoRepository.AddAsync(todo);
                _result.Status = true;
                _result.Message = "Görev Eklendi";
            }
            else
            {
                var todo = await _todoRepository.GetByIdAsync(model.Id);
                todo.Title = model.Title;
                todo.Description = model.Description;
                todo.IsOK = model.IsOK;
                await _todoRepository.UpdateAsync(todo);
                _result.Status = true;
                _result.Message = "Görev Güncellendi";

            }

            return Json(_result);
        }
    }
}
