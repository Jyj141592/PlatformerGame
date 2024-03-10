# 개요

PlayerController 테스용 프로젝트(에디터 버전 2022.3.18f1)

Jump King과 유사한 방식으로 점프를 통해 떨어지지 않고 위쪽으로 올라가는 게임

# 조작법

좌우 화살표로 이동, 스페이스바를 눌러 점프. 점프의 세기는 고정되어있고 스페이스바를 누르고 있는 시간에 따라 점프의 각도가 0~90도로 증가

# 게임 화면

![미디어1-_online-video-cutter com_](https://github.com/Jyj141592/PlatformerGame/assets/140074412/7c783742-cc01-4e37-80f6-553858e8a6ed)

# PlayerController

<img width="791" alt="스크린샷 2024-03-09 165556" src="https://github.com/Jyj141592/PlatformerGame/assets/140074412/2f46d916-f0d3-4ff9-8b3e-8a5506e4469b">

1. Idle : 가만히 움직이지 않을 때의 state

2. Walk : 좌우로 움직이고 있을 때의 state

3. Charge : 스페이스바를 누르고 있을 때의 state

4. Jump : 점프를 해 공중에 떠있을 때의 state. 매 프레임 이동 방향에 맞춰 플레이어 오브젝트의 각도를 설정

5. Ground : 땅에 착지했을 때의 state. 착지 애니메이션을 재생


