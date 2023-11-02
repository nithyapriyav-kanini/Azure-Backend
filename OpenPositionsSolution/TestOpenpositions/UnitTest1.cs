using Moq;
using OpenPositionsApp.Exceptions;
using OpenPositionsApp.Interfaces;
using OpenPositionsApp.Models.DTO;
using OpenPositionsApp.Models;
using OpenPositionsApp.Services;

namespace TestOpenpositions
{
    [TestClass]
    public class PositionServiceTests
    {
        private PositionService? _positionService;
        private Mock<IPositionRepo<OpenPosition, int>>? _positionRepoMock;
        private Mock<IBaseRepo<Location>>? _locationFetchingMock;
        private Mock<IAdapter>? _adapterMock;

        [TestInitialize]
        public void TestInitialize()
        {
            _positionRepoMock = new Mock<IPositionRepo<OpenPosition, int>>();
            _locationFetchingMock = new Mock<IBaseRepo<Location>>();
            _adapterMock = new Mock<IAdapter>();

            _positionService = new PositionService(
                _positionRepoMock.Object,
                _locationFetchingMock.Object,
                _adapterMock.Object
            );
        }

        [TestMethod]
        public async Task Add_ValidLocation_AddsPosition()
        {
            if (_locationFetchingMock!=null && _adapterMock!=null && _positionRepoMock!=null && _positionService!=null)
            {
                // Arrange
                var openPositionDto = new OpenPositionDTO
                {
                    PositionId = 0,
                    RoleName = "Developer",
                    Domain = "Engineering",
                    JobDescription = "Job description",
                    Location = "Chennai",
                    ReqSkills = "Skills",
                    EducationalQual = "Education",
                    Experience = 2
                };
                var adapterMappedPosition = new OpenPosition()
                {
                    PositionId = 0,
                    RoleName = "Developer",
                    Domain = "Engineering",
                    JobDescription = "Job description",
                    LocationId = 1,
                    ReqSkills = "Skills",
                    EducationalQual = "Education",
                    Experience = 2
                };
                var addedPosition = new OpenPosition
                {
                    PositionId = 1,
                    RoleName = "Developer",
                    Domain = "Engineering",
                    JobDescription = "Job description",
                    LocationId = 1,
                    ReqSkills = "Skills",
                    EducationalQual = "Education",
                    Experience = 2
                };
                var addedposition = new OpenPositionDTO
                {
                    PositionId = 1,
                    RoleName = "Developer",
                    Domain = "Engineering",
                    JobDescription = "Job description",
                    Location = "Chennai",
                    ReqSkills = "Skills",
                    EducationalQual = "Education",
                    Experience = 2
                };
                _adapterMock.Setup(repo => repo.Mapper("Chennai")).ReturnsAsync(1);
                _adapterMock.Setup(adapter => adapter.Mapper(openPositionDto)).ReturnsAsync(adapterMappedPosition);
                _positionRepoMock.Setup(repo => repo.Add(adapterMappedPosition)).ReturnsAsync(addedPosition);
                _adapterMock.Setup(adapter => adapter.Mapper(addedPosition)).ReturnsAsync(addedposition);

                // Act
                var result = await _positionService.Add(openPositionDto);

                // Assert
                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        public async Task Add_Exception_ValidLocation_AddsPosition()
        {
            if (_locationFetchingMock != null && _adapterMock != null && _positionRepoMock != null && _positionService != null)
            {
                // Arrange
                var openPositionDto = new OpenPositionDTO
                {
                    PositionId = 2,
                    RoleName = "Developer",
                    Domain = "Engineering",
                    JobDescription = "Job description",
                    Location = "Chennai",
                    ReqSkills = "Skills",
                    EducationalQual = "Education",
                    Experience = 2
                };
                var adapterMappedPosition = new OpenPosition()
                {
                    PositionId = 2,
                    RoleName = "Developer",
                    Domain = "Engineering",
                    JobDescription = "Job description",
                    LocationId = 1,
                    ReqSkills = "Skills",
                    EducationalQual = "Education",
                    Experience = 2
                };
                var addedPosition = new OpenPosition
                {
                    PositionId = 2,
                    RoleName = "Developer",
                    Domain = "Engineering",
                    JobDescription = "Job description",
                    LocationId = 1,
                    ReqSkills = "Skills",
                    EducationalQual = "Education",
                    Experience = 2
                };
                _adapterMock.Setup(repo => repo.Mapper("Chennai")).ReturnsAsync(1);
                _adapterMock.Setup(adapter => adapter.Mapper(openPositionDto)).ReturnsAsync(adapterMappedPosition);
                _positionRepoMock.Setup(repo => repo.Add(adapterMappedPosition)).ThrowsAsync(new DatabaseException());

                // Act
                var exception = await Assert.ThrowsExceptionAsync<DatabaseException>(() => _positionService.Add(openPositionDto));

                //Assert
                Assert.IsNotNull(exception);
            }
        }

        [TestMethod]
        public async Task Add_InvalidLocation_Null()
        {
            if (_positionService != null)
            {
                // Arrange
                var openPositionDto = new OpenPositionDTO()
                {
                    PositionId = 1,
                    RoleName = "Developer",
                    Domain = "Engineering",
                    JobDescription = "Job description",
                    Location = null,
                    ReqSkills = "Skills",
                    EducationalQual = "Education",
                    Experience = 2
                };

                // Act & Assert
                await Assert.ThrowsExceptionAsync<NullResultException>(() => _positionService.Add(openPositionDto));
            }
        }

        [TestMethod]
        public async Task Add_NoLocation_ThrowsInvalidLocationException()
        {
            if (_locationFetchingMock != null && _positionService != null)
            {
                // Arrange
                var openPositionDto = new OpenPositionDTO()
                {
                    PositionId = 1,
                    RoleName = "Developer",
                    Domain = "Engineering",
                    JobDescription = "Job description",
                    Location = null,
                    ReqSkills = "Skills",
                    EducationalQual = "Education",
                    Experience = 2
                };

                // Act & Assert
                await Assert.ThrowsExceptionAsync<NullResultException>(() => _positionService.Add(openPositionDto));
            }
        }

        [TestMethod]
        public async Task Delete_ValidKey_DeletesPosition()
        {
            if (_adapterMock != null && _positionRepoMock != null && _positionService != null)
            {
                // Arrange
                const int positionKey = 1;
                var addedPosition = new OpenPosition
                {
                    PositionId = 1,
                    RoleName = "Developer",
                    Domain = "Engineering",
                    JobDescription = "Job description",
                    LocationId = 1,
                    ReqSkills = "Skills",
                    EducationalQual = "Education",
                    Experience = 2
                };
                var position = new OpenPositionDTO
                {
                    PositionId = 1,
                    RoleName = "Developer",
                    Domain = "Engineering",
                    JobDescription = "Job description",
                    Location = "Chennai",
                    ReqSkills = "Skills",
                    EducationalQual = "Education",
                    Experience = 2
                };
                _positionRepoMock.Setup(repo => repo.Delete(positionKey)).ReturnsAsync(addedPosition);
                _adapterMock.Setup(adapter => adapter.Mapper(addedPosition)).ReturnsAsync(position);

                // Act
                var result = await _positionService.Delete(positionKey);

                // Assert
                Assert.IsNotNull(result);
            }
        }
        [TestMethod]
        public async Task Get_ValidKey_ReturnsPosition()
        {
            if (_adapterMock != null && _positionRepoMock != null && _positionService != null)
            {
                // Arrange
                const int validPositionId = 1;
                var openPosition = new OpenPosition { PositionId = validPositionId };
                var expectedPositionDTO = new OpenPositionDTO { PositionId = validPositionId };

                _positionRepoMock.Setup(repo => repo.Get(validPositionId)).ReturnsAsync(openPosition);
                _adapterMock.Setup(adapter => adapter.Mapper(openPosition)).ReturnsAsync(expectedPositionDTO);

                // Act
                var result = await _positionService.Get(validPositionId);

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(validPositionId, result.PositionId);
            }
        }

        [TestMethod]
        public async Task GetAll_NonEmptyResult_ReturnsListOfPositions()
        {
            if (_adapterMock != null && _positionRepoMock != null && _positionService != null)
            {
                // Arrange
                var openPositions = new List<OpenPosition>
                {
                new OpenPosition { PositionId = 1 },
                new OpenPosition { PositionId = 2 }
                };

                var expectedPositionDTOs = new List<OpenPositionDTO>
                {
                new OpenPositionDTO { PositionId = 1 },
                new OpenPositionDTO { PositionId = 2 }
                };

                _positionRepoMock.Setup(repo => repo.GetAll()).ReturnsAsync(openPositions);

                foreach (var openPosition in openPositions)
                {
                    _adapterMock.Setup(adapter => adapter.Mapper(openPosition)).ReturnsAsync(
                        expectedPositionDTOs.First(dto => dto.PositionId == openPosition.PositionId)
                    );
                }

                // Act
                var result = await _positionService.GetAll();

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(openPositions.Count, result.Count);
            }
        }

        [TestMethod]
        public async Task GetAll_EmptyList_ReturnsListOfPositions()
        {
            if (_adapterMock != null && _positionRepoMock != null && _positionService != null)
            {
                // Arrange
                var openPositions = new List<OpenPosition>
                {

                };

                _positionRepoMock.Setup(repo => repo.GetAll()).ReturnsAsync(openPositions);

                // Act & Assert
                await Assert.ThrowsExceptionAsync<NullResultException>(() => _positionService.GetAll());
            }
        }

        [TestMethod]
        public async Task GetAll_EmptyResult_ReturnsNull()
        {
            if (_adapterMock != null && _positionRepoMock != null && _positionService != null)
            {
                // Arrange
                _positionRepoMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<OpenPosition>());

                // Act & Assert
                await Assert.ThrowsExceptionAsync<NullResultException>(() => _positionService.GetAll());
            }
        }

        [TestMethod]
        public async Task Update_ValidLocationAndPosition_ReturnsUpdatedPosition()
        {
            if (_adapterMock != null && _positionRepoMock != null && _positionService != null&& _locationFetchingMock!=null)
            {
                // Arrange
                var openPositionDto = new OpenPositionDTO
                {
                    PositionId = 1,
                    RoleName = "Developer",
                    Domain = "Engineering",
                    JobDescription = "Job description",
                    Location = "Chennai",
                    ReqSkills = "Skills",
                    EducationalQual = "Education",
                    Experience = 2
                };

                _adapterMock.Setup(repo => repo.Mapper("Chennai")).ReturnsAsync(1);

                var adapterMappedPosition = new OpenPosition
                {
                    PositionId = 1,
                    RoleName = "Developer",
                    Domain = "Engineering",
                    JobDescription = "Job description",
                    LocationId = 1,
                    ReqSkills = "Skills",
                    EducationalQual = "Education",
                    Experience = 2
                };
                _adapterMock.Setup(adapter => adapter.Mapper(openPositionDto)).ReturnsAsync(adapterMappedPosition);

                var updatedPosition = new OpenPosition
                {
                    PositionId = 1,
                    RoleName = "Updated Role",
                    Domain = "Updated Domain",
                    JobDescription = "Updated Description",
                    LocationId = 1,
                    ReqSkills = "Updated Skills",
                    EducationalQual = "Updated Education",
                    Experience = 3
                };
                var openPositiondto = new OpenPositionDTO
                {
                    PositionId = 1,
                    RoleName = "Updated Role",
                    Domain = "Updated Domain",
                    JobDescription = "Updated Description",
                    Location = "Chennai",
                    ReqSkills = "Updated Skills",
                    EducationalQual = "Updated Education",
                    Experience = 3
                };
                _positionRepoMock.Setup(repo => repo.Update(adapterMappedPosition)).ReturnsAsync(updatedPosition);
                _adapterMock.Setup(adapter => adapter.Mapper(updatedPosition)).ReturnsAsync(openPositiondto);

                // Act
                var result = await _positionService.Update(openPositionDto);

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(updatedPosition.RoleName, result.RoleName);
            }
        }

        [TestMethod]
        public async Task Update_InvalidLocation_Null()
        {
            if (_positionService != null)
            {
                // Arrange
                var openPositionDto = new OpenPositionDTO()
                {
                    PositionId = 1,
                    RoleName = "Developer",
                    Domain = "Engineering",
                    JobDescription = "Job description",
                    Location = null,
                    ReqSkills = "Skills",
                    EducationalQual = "Education",
                    Experience = 2
                };
                //Act & Assert
                await Assert.ThrowsExceptionAsync<NullResultException>(() => _positionService.Update(openPositionDto));
            }
        }

        [TestMethod]
        public async Task GetAllLocations_NonEmptyLocations_ReturnsListOfLocations()
        {
            if (_positionService != null && _locationFetchingMock != null)
            {
                // Arrange
                var expectedLocations = new List<Location>
                {
                new Location()
                {
                    LocationId = 1,
                    LocationName = "chennai"
                },
                new Location() 
                {
                    LocationId =2,
                    LocationName = "coimbatore"
                }
                };

                _locationFetchingMock.Setup(repo => repo.GetAll()).ReturnsAsync(expectedLocations);

                // Act
                var result = await _positionService.GetAllLocations();

                // Assert
                Assert.IsNotNull(result);
            }
        }
        [TestMethod]
        public async Task GetAllLocations_NonEmptyLocations_ReturnsListLocations()
        {
            if (_positionService != null && _locationFetchingMock != null)
            {
                // Arrange
                var expectedLocations = new List<Location>
                {
                new Location()
                {
                    LocationId = 1,
                    LocationName = "chennai"
                },
                new Location()
                {
                    LocationId =2,
                    LocationName = null
                }
                };

                _locationFetchingMock.Setup(repo => repo.GetAll()).ReturnsAsync(expectedLocations);

                // Act
                var result = await _positionService.GetAllLocations();

                // Assert
                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        public async Task GetAllLocations_NonEmptyLocations_ReturnsNull()
        {
            if (_positionService != null && _locationFetchingMock != null)
            {
                // Arrange
                var expectedLocations = new List<Location>
                {
                new Location()
                {
                    LocationId = 1,
                    LocationName =null
                },
                new Location()
                {
                    LocationId =2,
                    LocationName = null
                }
                };

                _locationFetchingMock.Setup(repo => repo.GetAll()).ReturnsAsync(expectedLocations);

                //Act & Assert
                await Assert.ThrowsExceptionAsync<NullResultException>(() => _positionService.GetAllLocations());
            }
        }
    }
}