using System;
using System.Collections.Generic;
using System.Linq;
using LCARS.ViewModels.Screens;

namespace LCARS.Domain
{
    public class Screens : IScreens
    {
        private readonly Repository.IRepository<Models.Screens.Screen> _repository;

        public Screens(Repository.IRepository<Models.Screens.Screen> repository)
        {
            _repository = repository;
        }

        public IEnumerable<Screen> GetScreens(string filePath)
        {
            var screenData = _repository.GetList(filePath).ToList();

            return screenData.Select(screenItem => new Screen
            {
                Id = screenItem.Id,
                Name = screenItem.Name,
                Boards =
                    screenItem.Boards.Select(
                        s => new Board {Id = s.Id, CategoryId = (ViewModels.Boards) s.CategoryId, Argument = s.Argument})
                        .ToList()
            }).OrderBy(s => s.Id).ToList();
        }

        public bool UpdateScreen(string filePath, Screen screen)
        {
            var screens = _repository.GetList(filePath).ToList();

            var selectedScreen = screens.SingleOrDefault(q => q.Id == screen.Id);

            if (selectedScreen == null) // New item
            {
                screens.Add(new Models.Screens.Screen
                {
                    Id = screen.Id,
                    Name = screen.Name,
                    Boards =
                        screen.Boards.Select(s => new Models.Screens.Board { CategoryId = (int)s.CategoryId, Argument = s.Argument })
                            .ToList()
                });
            }
            else // Updated item
            {
                selectedScreen.Id = screen.Id;
                selectedScreen.Name = screen.Name;
                selectedScreen.Boards =
                    screen.Boards.Select(s => new Models.Screens.Board { CategoryId = (int)s.CategoryId, Argument = s.Argument }).ToList();
            }

            _repository.UpdateList(filePath, screens);

            return selectedScreen == null;
        }

        public void DeleteScreen(string filePath, int id)
        {
            var queries = _repository.GetList(filePath).ToList();

            queries.Remove(queries.SingleOrDefault(q => q.Id == id));

            _repository.UpdateList(filePath, queries);
        }

        public void AddBoard(string filePath, Board board)
        {
            var screens = _repository.GetList(filePath).ToList();

            var selectedScreen = screens.Single(q => q.Id == board.ScreenId);

            selectedScreen.Boards.Add(new Models.Screens.Board
            {
                Id =  board.Id,
                CategoryId = (int) board.CategoryId,
                Argument = board.Argument == null ? "" : board.Argument
            });

            _repository.UpdateList(filePath, screens);
        }

        public void DeleteBoard(string filePath, int screenId, string boardId)
        {
            var screens = _repository.GetList(filePath).ToList();

            var selectedScreen = screens.Single(s => s.Id == screenId);

            selectedScreen.Boards.Remove(selectedScreen.Boards.Single(b => b.Id == boardId));

            _repository.UpdateList(filePath, screens);
        }
    }
}