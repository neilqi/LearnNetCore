添加修改：
git add readme.txt

提交修改：
git commit -m "append GPL"

查看区别：
git diff HEAD -- readme.txt 

撤销修改：
git checkout -- readme.txt

删除文件:
git rm test.txt

删除文件和目录
git rm -r -n

查看日志：
git log --pretty=oneline
git log --graph --pretty=oneline --abbrev-commit

查看命令历史：
git reflog

回滚到历史版本。上一个版本就是HEAD^，上上一个版本就是HEAD^^，当然往上100个版本写100个^比较容易数不过来，所以写成HEAD~100
git reset --hard HEAD^

回滚到制定版本。版本号从版本日志中获取
git reset --hard 1094a

查看当前状态：
git status

链接远程仓库
git remote add origin git@github.com:neil_qi/learnnetcore.git

把本地库的所有内容推送到远程库
git push -u origin master (-u 参数表明需要验证，用于首次提交到github)
git push origin master（首次验证后再同步就不需要再加-u了）

从github克隆
git clone git@github.com:neil_qi/learnnetcore.git

创建分支：
git checkout dev

创建并切换分支：
git branch -b dev

查看分支：
git branch

切换到分支：
git checkout dev

合并某分支到当前分支：
git merge dev

合并分支，带注释
git merge --no-ff -m "merge with no-ff" dev

强行删除分支：
git branch -D feature-vulcan

储藏（暂存）：
git stash
查看储藏：
git stash list
恢复使用储藏
git stash apply
如果有多个，制定使用哪个
git stash apply stash@{0} 

恢复储藏并删除
git stash pop

查看远程库的信息
git remote -v

从远程抓取，相当于svn的更新
git pull

指定本地dev分支与远程origin/dev分支的链接
git branch --set-upstream-to=origin/dev dev


rebase操作可以把本地未push的分叉提交历史整理成直线；
rebase的目的是使得我们在查看历史提交的变化时更容易，因为分叉的提交需要三方对比
git rebase

删除远程分支: 
git push origin --delete [branchname]

打标签（标签打在最近一次commit上）：
git tag v1.0

给历史commit打标签， -m参数是标签的说明
$ git tag v0.9 f52c633
$ git tag -a v0.1 -m "version 0.1 released" 1094adb

查看标签：
git tag

查看标签详情：
git show v0.9

删除标签：
git tag -d v0.1

把指定标签推到github
git push origin v1.0

一次性推送全部标签
git push origin --tags


删除远程标签：
git tag -d v0.9				（先删除本地标签）
git push origin :refs/tags/v0.9		（后删除远程标签）

强行添加文件
git add -f App.class

检查gitignore文件是否有错
git check-ignore