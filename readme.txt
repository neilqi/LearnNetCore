����޸ģ�
git add readme.txt

�ύ�޸ģ�
git commit -m "append GPL"

�鿴����
git diff HEAD -- readme.txt 

�����޸ģ�
git checkout -- readme.txt

ɾ���ļ�:
git rm test.txt

�鿴��־��
git log --pretty=oneline
git log --graph --pretty=oneline --abbrev-commit

�鿴������ʷ��
git reflog

�ع�����ʷ�汾����һ���汾����HEAD^������һ���汾����HEAD^^����Ȼ����100���汾д100��^�Ƚ�������������������д��HEAD~100
git reset --hard HEAD^

�ع����ƶ��汾���汾�ŴӰ汾��־�л�ȡ
git reset --hard 1094a

�鿴��ǰ״̬��
git status

����Զ�ֿ̲�
git remote add origin git@github.com:neil_qi/learnnetcore.git

�ѱ��ؿ�������������͵�Զ�̿�
git push -u origin master (-u ����������Ҫ��֤�������״��ύ��github)
git push origin master���״���֤����ͬ���Ͳ���Ҫ�ټ�-u�ˣ�

��github��¡
git clone git@github.com:neil_qi/learnnetcore.git

������֧��
git checkout dev

�������л���֧��
git branch -b dev

�鿴��֧��
git branch

�л�����֧��
git checkout dev

�ϲ�ĳ��֧����ǰ��֧��
git merge dev

�ϲ���֧����ע��
git merge --no-ff -m "merge with no-ff" dev

ǿ��ɾ����֧��
git branch -D feature-vulcan

���أ��ݴ棩��
git stash
�鿴���أ�
git stash list
�ָ�ʹ�ô���
git stash apply
����ж�����ƶ�ʹ���ĸ�
git stash apply stash@{0} 

�ָ����ز�ɾ��
git stash pop

�鿴Զ�̿����Ϣ
git remote -v

��Զ��ץȡ���൱��svn�ĸ���
git pull

ָ������dev��֧��Զ��origin/dev��֧������
git branch --set-upstream-to=origin/dev dev


rebase�������԰ѱ���δpush�ķֲ��ύ��ʷ�����ֱ�ߣ�
rebase��Ŀ����ʹ�������ڲ鿴��ʷ�ύ�ı仯ʱ�����ף���Ϊ�ֲ���ύ��Ҫ�����Ա�
git rebase