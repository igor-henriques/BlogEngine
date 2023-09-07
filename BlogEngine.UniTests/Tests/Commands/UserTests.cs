using BlogEngine.Domain.Commands.User.Create;
using BlogEngine.Domain.Commands.User.PatchPassword;
using BlogEngine.Domain.Commands.User.PatchRole;

namespace BlogEngine.UniTests.Tests.Commands;

public sealed class UserTests
{
    [Fact]
    public async Task Handle_WhenCalled_ShouldUpdateUserRole()
    {
        // Arrange
        var fakeUserId = Guid.NewGuid();
        var fakeRole = EUserRole.Writer;
        var fakeUser = new User { Id = fakeUserId, Role = EUserRole.Writer };

        var repoMock = new Mock<IBasePersistanceRepository<User>>();
        repoMock.Setup(repo => repo.GetUniqueAsync(fakeUserId, CancellationToken.None)).ReturnsAsync(fakeUser);

        var handler = new PatchRoleCommandHandler(repoMock.Object);

        // Act
        await handler.Handle(new PatchRoleCommand { UserId = fakeUserId, Role = fakeRole }, CancellationToken.None);

        // Assert
        repoMock.Verify(repo => repo.UpdateAsync(It.Is<User>(user => user.Role == fakeRole), CancellationToken.None), Times.Once);
    }

    [Fact]
    public void Validate_WhenUserIdIsEmpty_ShouldHaveValidationErrors()
    {
        // Arrange
        var validator = new PatchRoleCommandValidator();
        var command = new PatchRoleCommand { UserId = Guid.Empty, Role = EUserRole.Writer };

        // Act
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().Contain(err => err.PropertyName == nameof(command.UserId));
    }

    [Fact]
    public async Task Handle_WhenCalled_ShouldUpdateUserPassword()
    {
        // Arrange
        var fakeUserId = Guid.NewGuid();
        var fakePassword = "NewPassword123!";
        var fakeHashedPassword = "hashedPassword";
        var fakeUser = new User { Id = fakeUserId, HashedPassword = "oldHashedPassword" };

        var repoMock = new Mock<IBasePersistanceRepository<User>>();
        repoMock.Setup(repo => repo.GetUniqueAsync(fakeUserId, CancellationToken.None)).ReturnsAsync(fakeUser);

        var passwordHashingMock = new Mock<IPasswordHashingService>();
        passwordHashingMock.Setup(svc => svc.HashPassword(fakePassword)).Returns(fakeHashedPassword);

        var handler = new PatchPasswordCommandHandler(repoMock.Object, passwordHashingMock.Object);

        // Act
        await handler.Handle(new PatchPasswordCommand { UserId = fakeUserId, Password = fakePassword }, CancellationToken.None);

        // Assert
        repoMock.Verify(repo => repo.UpdateAsync(It.Is<User>(user => user.HashedPassword == fakeHashedPassword), CancellationToken.None), Times.Once);
    }

    [Fact]
    public void Validate_WhenPasswordIsInvalid_ShouldHaveValidationErrors()
    {
        // Arrange
        var validator = new PatchPasswordCommandValidator();
        var command = new PatchPasswordCommand { UserId = Guid.NewGuid(), Password = "invalid" };

        // Act
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().Contain(err => err.PropertyName == nameof(command.Password));
    }

    [Fact]
    public async Task Handle_WhenCalled_ShouldCreateUser()
    {
        // Arrange
        var fakeName = "fakeName";
        var fakeEmail = "email@example.com";
        var fakePassword = "Password123!";
        var fakeHashedPassword = "hashedPassword";

        var user = new User(fakeName, fakeEmail, fakeHashedPassword, EUserRole.Writer);

        var repoMock = new Mock<IBasePersistanceRepository<User>>();
        var passwordHashingMock = new Mock<IPasswordHashingService>();

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(mapper => mapper.Map<User>(It.IsAny<CreateUserCommand>()))
          .Returns(new User { Email = fakeEmail, HashedPassword = fakeHashedPassword, Role = EUserRole.Writer });


        passwordHashingMock.Setup(svc => svc.HashPassword(fakePassword)).Returns(fakeHashedPassword);

        var handler = new CreateUserCommandHandler(repoMock.Object, mapperMock.Object, passwordHashingMock.Object);

        // Act
        var result = await handler.Handle(new CreateUserCommand 
        { 
            Email = fakeEmail, 
            Password = fakePassword 
        }, CancellationToken.None);

        // Assert
        repoMock.Verify(repo => repo.AddAsync(It.Is<User>(user => user.Email == fakeEmail && user.HashedPassword == fakeHashedPassword), CancellationToken.None), Times.Once);
    }

    [Fact]
    public void Validate_WhenEmailIsEmpty_ShouldHaveValidationErrors()
    {
        // Arrange
        var validator = new CreateUserCommandValidator();
        var command = new CreateUserCommand { Email = "", Password = "Password123!" };

        // Act
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().Contain(err => err.PropertyName == nameof(command.Email));
    }
}
