/*
Navicat MySQL Data Transfer

Source Server         : MySql
Source Server Version : 50713
Source Host           : localhost:3306
Source Database       : pc.information.db

Target Server Type    : MYSQL
Target Server Version : 50713
File Encoding         : 65001

Date: 2017-01-21 12:37:04
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for piferrorlog
-- ----------------------------
DROP TABLE IF EXISTS `piferrorlog`;
CREATE TABLE `piferrorlog` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `ContentType` varchar(255) DEFAULT NULL COMMENT 'Request contentType',
  `ErrorMessage` mediumtext COMMENT 'error message',
  `InnerErrorMessage` mediumtext COMMENT 'Inner message.',
  `ErrorTypeFullName` mediumtext COMMENT 'Error type full name',
  `StackTrace` mediumtext COMMENT 'Error stack trace',
  `ErrorTime` datetime NOT NULL COMMENT 'product error time',
  `ErrorType` int(11) NOT NULL DEFAULT '0' COMMENT 'error type enum 0:error;1:warning;2:info',
  PRIMARY KEY (`Id`,`ErrorTime`,`ErrorType`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of piferrorlog
-- ----------------------------
INSERT INTO `piferrorlog` VALUES ('1', 'text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8', 'Sequence contains no elements', null, 'System.InvalidOperationException', '   at System.Linq.Enumerable.First[TSource](IEnumerable`1 source)\r\n   at Dapper.SqlMapper.QueryRowImpl[T](IDbConnection cnn, Row row, CommandDefinition& command, Type effectiveType)\r\n   at Dapper.SqlMapper.QueryFirst[T](IDbConnection cnn, String sql, Object param, IDbTransaction transaction, Nullable`1 commandTimeout, Nullable`1 commandType)\r\n   at Pc.Information.Utility.FreshSqlHelper.FreshSqlHelper.FindOne[T](String cmd, DynamicParameters param, Boolean flag, String connection)\r\n   at Pc.Information.DataAccess.UserInfoDataAccess.UserInfoDataAccess.GetUserInfo(String userName, String password)\r\n   at Pc.Information.Business.UserInfoBll.UserInfoBll.GetUserInfo(String username, String password)\r\n   at Pc.Information.Api.Controllers.LoginUserController.Login(String userName, String password)\r\n   at lambda_method(Closure , Object , Object[] )\r\n   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeActionFilterAsync>d__28.MoveNext()\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeAsync>d__18.MoveNext()\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)\r\n   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)\r\n   at Microsoft.AspNetCore.Builder.RouterMiddleware.<Invoke>d__4.MoveNext()\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)\r\n   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)\r\n   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()\r\n   at Pc.Information.Api.MiddleWares.ExceptionHandlerMiddleWare.<Invoke>d__3.MoveNext()', '2017-01-14 20:08:48', '0');
INSERT INTO `piferrorlog` VALUES ('2', 'text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8', 'Sequence contains no elements', null, 'System.InvalidOperationException', '   at System.Linq.Enumerable.First[TSource](IEnumerable`1 source)\r\n   at Dapper.SqlMapper.QueryRowImpl[T](IDbConnection cnn, Row row, CommandDefinition& command, Type effectiveType)\r\n   at Dapper.SqlMapper.QueryFirst[T](IDbConnection cnn, String sql, Object param, IDbTransaction transaction, Nullable`1 commandTimeout, Nullable`1 commandType)\r\n   at Pc.Information.Utility.FreshSqlHelper.FreshSqlHelper.FindOne[T](String cmd, DynamicParameters param, Boolean flag, String connection)\r\n   at Pc.Information.DataAccess.UserInfoDataAccess.UserInfoDataAccess.GetUserInfo(String userName, String password)\r\n   at Pc.Information.Business.UserInfoBll.UserInfoBll.GetUserInfo(String username, String password)\r\n   at Pc.Information.Api.Controllers.LoginUserController.Login(String userName, String password)\r\n   at lambda_method(Closure , Object , Object[] )\r\n   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeActionFilterAsync>d__28.MoveNext()\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeAsync>d__18.MoveNext()\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)\r\n   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)\r\n   at Microsoft.AspNetCore.Builder.RouterMiddleware.<Invoke>d__4.MoveNext()\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)\r\n   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)\r\n   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()\r\n   at Pc.Information.Api.MiddleWares.ExceptionHandlerMiddleWare.<Invoke>d__3.MoveNext()', '2017-01-14 20:31:17', '0');
INSERT INTO `piferrorlog` VALUES ('3', 'text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8', 'Sequence contains no elements', null, 'System.InvalidOperationException', '   at System.Linq.Enumerable.First[TSource](IEnumerable`1 source)\r\n   at Dapper.SqlMapper.QueryRowImpl[T](IDbConnection cnn, Row row, CommandDefinition& command, Type effectiveType)\r\n   at Dapper.SqlMapper.QueryFirst[T](IDbConnection cnn, String sql, Object param, IDbTransaction transaction, Nullable`1 commandTimeout, Nullable`1 commandType)\r\n   at Pc.Information.Utility.FreshSqlHelper.FreshSqlHelper.FindOne[T](String cmd, DynamicParameters param, Boolean flag, String connection)\r\n   at Pc.Information.DataAccess.UserInfoDataAccess.UserInfoDataAccess.GetUserInfo(String userName, String password)\r\n   at Pc.Information.Business.UserInfoBll.UserInfoBll.GetUserInfo(String username, String password)\r\n   at Pc.Information.Api.Controllers.LoginUserController.Login(String userName, String password)\r\n   at lambda_method(Closure , Object , Object[] )\r\n   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeActionFilterAsync>d__28.MoveNext()\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeAsync>d__18.MoveNext()\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)\r\n   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)\r\n   at Microsoft.AspNetCore.Builder.RouterMiddleware.<Invoke>d__4.MoveNext()\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)\r\n   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)\r\n   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()\r\n   at Pc.Information.Api.MiddleWares.ExceptionHandlerMiddleWare.<Invoke>d__3.MoveNext()', '2017-01-14 20:32:34', '0');
INSERT INTO `piferrorlog` VALUES ('4', null, 'There is already an open DataReader associated with this Connection which must be closed first.', null, 'MySql.Data.MySqlClient.MySqlException', '   at MySql.Data.MySqlClient.MySqlConnection.Throw(Exception ex)\r\n   at MySql.Data.MySqlClient.MySqlCommand.Throw(Exception ex)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReader(CommandBehavior behavior)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReader()\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery()\r\n   at MySql.Data.MySqlClient.MySqlCommand.ResetSqlSelectLimit()\r\n   at MySql.Data.MySqlClient.MySqlCommand.Close(MySqlDataReader reader)\r\n   at MySql.Data.MySqlClient.MySqlDataReader.Close()\r\n   at MySql.Data.MySqlClient.MySqlDataReader.Dispose(Boolean disposing)\r\n   at MySql.Data.MySqlClient.MySqlDataReader.Dispose()\r\n   at Dapper.SqlMapper.QueryRowImpl[T](IDbConnection cnn, Row row, CommandDefinition& command, Type effectiveType)\r\n   at Dapper.SqlMapper.QueryFirstOrDefault[T](IDbConnection cnn, String sql, Object param, IDbTransaction transaction, Nullable`1 commandTimeout, Nullable`1 commandType)\r\n   at Pc.Information.Utility.FreshSqlHelper.FreshSqlHelper.FindOne[T](String cmd, DynamicParameters param, Boolean flag, String connection)\r\n   at Pc.Information.DataAccess.UserInfoDataAccess.UserInfoDataAccess.GetUserInfo(String userName, String password)\r\n   at Pc.Information.Business.UserInfoBll.UserInfoBll.GetUserInfo(String username, String password)\r\n   at Pc.Information.Api.Controllers.LoginUserController.Login(String userName, String password)\r\n   at lambda_method(Closure , Object , Object[] )\r\n   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeActionFilterAsync>d__28.MoveNext()\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeAsync>d__18.MoveNext()\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)\r\n   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)\r\n   at Microsoft.AspNetCore.Builder.RouterMiddleware.<Invoke>d__4.MoveNext()\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)\r\n   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)\r\n   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()\r\n   at Pc.Information.Api.MiddleWares.ExceptionHandlerMiddleWare.<Invoke>d__3.MoveNext()', '2017-01-15 17:07:44', '0');
INSERT INTO `piferrorlog` VALUES ('5', 'text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8', 'Authentication to host \'127.0.0.1\' for user \'root\' using method \'mysql_native_password\' failed with message: Access denied for user \'root\'@\'localhost\' (using password: YES)', 'Access denied for user \'root\'@\'localhost\' (using password: YES)', 'MySql.Data.MySqlClient.MySqlException', '   at MySql.Data.MySqlClient.Authentication.MySqlAuthenticationPlugin.AuthenticationFailed(Exception ex)\r\n   at MySql.Data.MySqlClient.Authentication.MySqlAuthenticationPlugin.ReadPacket()\r\n   at MySql.Data.MySqlClient.Authentication.MySqlAuthenticationPlugin.Authenticate(Boolean reset)\r\n   at MySql.Data.MySqlClient.NativeDriver.Authenticate(String authMethod, Boolean reset)\r\n   at MySql.Data.MySqlClient.NativeDriver.Open()\r\n   at MySql.Data.MySqlClient.Driver.Open()\r\n   at MySql.Data.MySqlClient.Driver.Create(MySqlConnectionStringBuilder settings)\r\n   at MySql.Data.MySqlClient.MySqlPool.CreateNewPooledConnection()\r\n   at MySql.Data.MySqlClient.MySqlPool.GetPooledConnection()\r\n   at MySql.Data.MySqlClient.MySqlPool.TryToGetDriver()\r\n   at MySql.Data.MySqlClient.MySqlPool.GetConnection()\r\n   at MySql.Data.MySqlClient.MySqlConnection.Open()\r\n   at Dapper.SqlMapper.QueryRowImpl[T](IDbConnection cnn, Row row, CommandDefinition& command, Type effectiveType)\r\n   at Dapper.SqlMapper.QueryFirstOrDefault[T](IDbConnection cnn, String sql, Object param, IDbTransaction transaction, Nullable`1 commandTimeout, Nullable`1 commandType)\r\n   at Pc.Information.Utility.FreshSqlHelper.FreshSqlHelper.FindOne[T](String cmd, DynamicParameters param, Boolean flag, String connection)\r\n   at Pc.Information.DataAccess.UserInfoDataAccess.UserInfoDataAccess.GetUserInfo(String userName, String password)\r\n   at Pc.Information.Business.UserInfoBll.UserInfoBll.GetUserInfo(String username, String password)\r\n   at Pc.Information.Api.Controllers.LoginUserController.Login(String userName, String password)\r\n   at lambda_method(Closure , Object , Object[] )\r\n   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeActionFilterAsync>d__28.MoveNext()\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeAsync>d__18.MoveNext()\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)\r\n   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)\r\n   at Microsoft.AspNetCore.Builder.RouterMiddleware.<Invoke>d__4.MoveNext()\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)\r\n   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)\r\n   at Swashbuckle.SwaggerUi.Application.SwaggerUiMiddleware.<Invoke>d__5.MoveNext()\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)\r\n   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)\r\n   at Swashbuckle.SwaggerUi.Application.RedirectMiddleware.<Invoke>d__4.MoveNext()\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)\r\n   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)\r\n   at Swashbuckle.Swagger.Application.SwaggerMiddleware.<Invoke>d__6.MoveNext()\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)\r\n   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)\r\n   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()\r\n   at Pc.Information.Api.MiddleWares.ExceptionHandlerMiddleWare.<Invoke>d__3.MoveNext()', '2017-01-21 11:02:13', '0');

-- ----------------------------
-- Table structure for pifgroup
-- ----------------------------
DROP TABLE IF EXISTS `pifgroup`;
CREATE TABLE `pifgroup` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `PiFGroupNick` varchar(255) DEFAULT '' COMMENT '群名称',
  `PiFCreateTime` datetime DEFAULT NULL COMMENT '群创建时间',
  `PiFGroupTopic` varchar(255) DEFAULT '' COMMENT '群主题',
  `PiFIsValid` int(11) DEFAULT '0' COMMENT '是否解散（1：是；0：否）',
  `PiFGroupCount` int(255) DEFAULT '100' COMMENT '群容量',
  `PiFGroupBelongId` int(11) DEFAULT NULL COMMENT '群主id',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of pifgroup
-- ----------------------------

-- ----------------------------
-- Table structure for pifgroupmember
-- ----------------------------
DROP TABLE IF EXISTS `pifgroupmember`;
CREATE TABLE `pifgroupmember` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `PiFGroupNick` varchar(255) DEFAULT '' COMMENT '群名称',
  `PiFGroupId` int(11) DEFAULT '0' COMMENT '群id',
  `PiFIsValid` int(11) DEFAULT '1' COMMENT '是否是有效成员（1：有效；0：无效）',
  `PiFJoinTime` datetime DEFAULT NULL COMMENT '成员加入时间',
  `PiFUserId` int(11) DEFAULT '0' COMMENT '用户id',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of pifgroupmember
-- ----------------------------

-- ----------------------------
-- Table structure for pifinformationlog
-- ----------------------------
DROP TABLE IF EXISTS `pifinformationlog`;
CREATE TABLE `pifinformationlog` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `PiFFromId` int(11) DEFAULT NULL COMMENT '发送者Id',
  `PiFToId` int(11) DEFAULT '0' COMMENT '接受者',
  `PiFToGroupId` int(11) DEFAULT '0' COMMENT '群消息对应的群id',
  `PiFContentType` int(255) DEFAULT '0' COMMENT '消息内容类型（0：文字；1：图片；）',
  `PiFContent` mediumtext COMMENT '消息内容，文字，图片base64',
  `PiFSendTime` datetime DEFAULT NULL COMMENT '发送时间',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of pifinformationlog
-- ----------------------------

-- ----------------------------
-- Table structure for pifquestioninfo
-- ----------------------------
DROP TABLE IF EXISTS `pifquestioninfo`;
CREATE TABLE `pifquestioninfo` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `PiFQuestionTitle` varchar(255) DEFAULT '' COMMENT '问题标题',
  `PiFQuestionContent` varchar(255) DEFAULT '' COMMENT '问题内容',
  `PiFCreateTime` datetime DEFAULT NULL COMMENT '创建时间',
  `PiFSendUserId` int(11) DEFAULT NULL COMMENT '用户id',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of pifquestioninfo
-- ----------------------------

-- ----------------------------
-- Table structure for pifquestionreplyinfo
-- ----------------------------
DROP TABLE IF EXISTS `pifquestionreplyinfo`;
CREATE TABLE `pifquestionreplyinfo` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `PiFQuestionId` int(11) DEFAULT '0' COMMENT '问题id',
  `PiFReplyContent` varchar(255) DEFAULT '' COMMENT '回复内容',
  `PiFReplyIsBest` int(11) DEFAULT '0' COMMENT '是否最佳答案（1：是；0：否）',
  `PiFReplyTime` datetime DEFAULT NULL COMMENT '回复时间',
  `PiFReplyUserId` int(11) DEFAULT '0' COMMENT '回复用户id',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of pifquestionreplyinfo
-- ----------------------------

-- ----------------------------
-- Table structure for pifreplypraisedinfo
-- ----------------------------
DROP TABLE IF EXISTS `pifreplypraisedinfo`;
CREATE TABLE `pifreplypraisedinfo` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `PiFUerId` int(11) DEFAULT '0' COMMENT '点赞用户id',
  `PiFPraisedTime` datetime DEFAULT NULL COMMENT '点赞时间',
  `PiFReplyId` int(11) DEFAULT '0' COMMENT '对应的回复内容id',
  `PiFPraisedType` int(255) DEFAULT '1' COMMENT '是否点赞（1：赞；0：踩）',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of pifreplypraisedinfo
-- ----------------------------

-- ----------------------------
-- Table structure for pifusers
-- ----------------------------
DROP TABLE IF EXISTS `pifusers`;
CREATE TABLE `pifusers` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `PiFSex` int(11) DEFAULT '0' COMMENT '性别（0：女；1：男；2：其他）',
  `PiFUserName` varchar(255) NOT NULL DEFAULT '' COMMENT '用户名',
  `PiFPassword` varchar(255) DEFAULT '' COMMENT '用户密码',
  `PiFRule` int(255) DEFAULT '0' COMMENT '角色（0：普通用户；1：普通服务者；2：管理员）',
  `PiFJob` varchar(255) DEFAULT '' COMMENT '职业',
  `PiFEmailAddress` varchar(255) DEFAULT '' COMMENT '邮箱地址',
  `PiFBirthday` datetime DEFAULT NULL COMMENT '出生年月',
  `PiFRegisterTime` datetime DEFAULT NULL COMMENT '注册时间',
  PRIMARY KEY (`Id`,`PiFUserName`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of pifusers
-- ----------------------------
INSERT INTO `pifusers` VALUES ('1', '1', 'FreshMan', '71f099ce7cb57b9ffa79ff5ae30c4ade', '2', '高级软件工程师', 'qinbocai@sina.cn', '1991-06-07 12:22:52', '2017-01-08 12:23:17');
INSERT INTO `pifusers` VALUES ('2', '0', 'PaiPai', '71f099ce7cb57b9ffa79ff5ae30c4ade', '1', '开发助理', 'chenlu@ly.com', '1995-06-23 00:00:00', '2017-01-08 12:23:17');
INSERT INTO `pifusers` VALUES ('3', '1', 'qinbocai', '2', '2', '实习生', 'shi@ly.com', null, null);
INSERT INTO `pifusers` VALUES ('4', '1', 'test', '17f6aec98cd238a8b2e5679839113bb4', '1', '测试工程师', 'qingzhan@gmail.com', '2000-12-09 00:00:00', '2017-01-12 00:00:00');
INSERT INTO `pifusers` VALUES ('5', '1', 'langyuelei', 'langyuelei', '1', '前端开发助理', 'langyuelei@163.com', '1994-05-03 00:00:00', '2017-01-12 20:20:29');
INSERT INTO `pifusers` VALUES ('6', '0', 'wangli', 'wangli', '1', '前端开发助理', 'wangli@163.com', '1994-05-02 00:00:00', '2017-01-12 20:20:29');
