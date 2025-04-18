
当前项目使用.NET8搭建 使用了DDD 分层架构、充血模型，额外使用了AutoMapper，FluentValidation技术

项目结构

Bookstore.Api				  API层

Bookstore.Application      应用服务层  (dto, AutoMapper配置，FluentValidation配置)

Bookstore.Domain           领域模型层（实体（充血模型）,领域业务）

Bookstore.Infrastructure  基础设施层 （仓储实现，身份验证和数据库配置）


发布后使用 dotnet Bookstore.API.dll 运行 ，使用/swagger/index.html可查看接口，注册后登录获取token ，在swagger里面直接绑定token可获取所有接口的访问权限
